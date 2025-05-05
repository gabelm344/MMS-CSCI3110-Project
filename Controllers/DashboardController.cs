using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CSCI3110_TermProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ISongRepository _songRepo;
        private readonly IPlaylistRepository _playlistRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            ISongRepository songRepo,
            IPlaylistRepository playlistRepo,
            UserManager<ApplicationUser> userManager)
        {
            _songRepo = songRepo;
            _playlistRepo = playlistRepo;
            _userManager = userManager;
        }

        // GET: /Dashboard
        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                TotalSongs = (await _songRepo.GetAllAsync()).Count(),
                TotalPlaylists = (await _playlistRepo.GetAllAsync()).Count(),
                TotalUsers = _userManager.Users.Count()
            };
            return View(vm);
        }
    }
}