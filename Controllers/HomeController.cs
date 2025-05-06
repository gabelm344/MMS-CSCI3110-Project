using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CSCI3110_TermProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISongRepository _songRepo;
        private readonly IPlaylistRepository _playlistRepo;

        public HomeController(ISongRepository songRepo, IPlaylistRepository playlistRepo)
        {
            _songRepo = songRepo;
            _playlistRepo = playlistRepo;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var songs = await _songRepo.GetAllAsync();
            var playlists = await _playlistRepo.GetAllAsync();
            var vm = new HomeViewModel
            {
                Songs = songs,
                Playlists = playlists
            };
            return View(vm);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}