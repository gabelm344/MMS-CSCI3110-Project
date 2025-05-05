using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CSCI3110_TermProject.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistRepository _playlistRepo;
        private readonly ISongRepository _songRepo;

        public PlaylistsController(
            IPlaylistRepository playlistRepo,
            ISongRepository songRepo)
        {
            _playlistRepo = playlistRepo;
            _songRepo = songRepo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Playlist playlist)
        {
            // Assign current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            playlist.UserId = userId;
            ModelState.Remove(nameof(playlist.UserId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _playlistRepo.AddAsync(playlist);

            // If AJAX request, return created playlist data as JSON
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { id = playlist.Id, name = playlist.Name, userId = playlist.UserId });
            }

            // Fallback for normal form post
            return RedirectToAction("Index", "Home");
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // 1) Load the playlist with its songs
            var playlist = await _playlistRepo.GetByIdAsync(id.Value);
            if (playlist == null)
                return NotFound();

            // 2) Load all songs for the “Add Song” dropdown
            var allSongs = await _songRepo.GetAllAsync();
            ViewBag.Songs = allSongs;

            return View(playlist);
        }

        // POST: Playlists/AddSong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSong(int playlistId, int songId)
        {
            await _playlistRepo.AddSongToPlaylistAsync(playlistId, songId);
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        // POST: Playlists/RemoveSong
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSong(int playlistId, int songId)
        {
            await _playlistRepo.RemoveSongFromPlaylistAsync(playlistId, songId);
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        // POST: Playlists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var playlist = await _playlistRepo.GetByIdAsync(id.Value);
            if (playlist == null) return NotFound();

            return View(playlist);
        }

        // POST: Playlists/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Playlist playlist)
        {
            if (id != playlist.Id) return NotFound();

            // re-assign userId to prevent validation errors
            playlist.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove(nameof(playlist.UserId));

            if (!ModelState.IsValid)
                return View(playlist);

            await _playlistRepo.UpdateAsync(playlist);
            return RedirectToAction("Index", "Home");
        }

        // POST: Playlists/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _playlistRepo.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }


    }
}