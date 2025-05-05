using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110_TermProject.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongRepository _songRepo;

        public SongsController(ISongRepository songRepo)
        {
            _songRepo = songRepo;
        }


        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var songs = await _songRepo.GetAllAsync();
            return View(songs);
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var song = await _songRepo.GetByIdAsync(id.Value);
            if (song == null) return NotFound();

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Artist,Album,Genre")] Song song, IFormFile file)
        {
            // Debug: confirm we hit the action
            Console.WriteLine($"[CTRL] Create POST hit: Title='{song.Title}', FileProvided={file != null}");

            // 1) Handle file upload first, so FilePath is populated before validation
            if (file != null && file.Length > 0)
            {
                // Ensure the upload folder exists
                var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "music");
                Directory.CreateDirectory(uploadsRoot);

                // Generate a unique filename and save
                var uniqueName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var fullPath = Path.Combine(uploadsRoot, uniqueName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                song.FilePath = $"/music/{uniqueName}";
                Console.WriteLine($"[CTRL] Assigned FilePath: {song.FilePath}");
            }

            // 2) Validate now that FilePath is set (nullable in model)
            if (!ModelState.IsValid)
            {
                // Debug: list validation errors
                var errors = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage);
                Console.WriteLine("[CTRL] ModelState invalid: " + string.Join("; ", errors));
                return View(song);
            }

            // 3) Save to the database via your repository
            Console.WriteLine($"[CTRL] Saving Song: {song.Title}");
            await _songRepo.AddAsync(song);
            Console.WriteLine("[CTRL] Save complete");

            // 4) Redirect back to Home index (where songs are listed)
            return RedirectToAction("Index", "Home");
        }


        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var song = await _songRepo.GetByIdAsync(id.Value);
            if (song == null) return NotFound();

            return View(song);
        }

        // POST: Songs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,Album,Genre,FilePath")] Song song)
        {
            if (id != song.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(song);

            await _songRepo.UpdateAsync(song);
            return RedirectToAction("Index", "Home");
        }


        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var song = await _songRepo.GetByIdAsync(id.Value);
            if (song == null) return NotFound();

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _songRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SongExists(int id)
        {
            var song = await _songRepo.GetByIdAsync(id);
            return song != null;
        }
    }
}
