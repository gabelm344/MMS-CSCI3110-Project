using System.Security.Claims;
using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CSCI3110_TermProject.Controllers
{
    [Authorize]
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

        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Playlist playlist)
        {
            // assign current user
            playlist.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove(nameof(playlist.UserId));

            if (!ModelState.IsValid)
                return View(playlist);

            await _playlistRepo.AddAsync(playlist);
            return RedirectToAction("Index", "Home");
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var playlist = await _playlistRepo.GetByIdAsync(id.Value);
            if (playlist == null) return NotFound();

            // load all songs for the “Add” dropdown
            ViewBag.Songs = await _songRepo.GetAllAsync();
            return View(playlist);
        }

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

            playlist.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove(nameof(playlist.UserId));

            if (!ModelState.IsValid)
                return View(playlist);

            await _playlistRepo.UpdateAsync(playlist);
            return RedirectToAction("Index", "Home");
        }

        //GET: Playlists/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var playlist = await _playlistRepo.GetByIdAsync(id.Value);
            if (playlist == null) return NotFound();
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _playlistRepo.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }

        // POST: Playlists/AddSong
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSong(int playlistId, int songId)
        {
            await _playlistRepo.AddSongToPlaylistAsync(playlistId, songId);
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }

        // POST: Playlists/RemoveSong
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveSong(int playlistId, int songId)
        {
            await _playlistRepo.RemoveSongFromPlaylistAsync(playlistId, songId);
            return RedirectToAction(nameof(Details), new { id = playlistId });
        }
    }
}
