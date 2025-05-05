using CSCI3110_TermProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110_TermProject.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsApiController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepo;

        public PlaylistsApiController(IPlaylistRepository playlistRepo)
        {
            _playlistRepo = playlistRepo;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> Get()
        {
            var playlists = await _playlistRepo.GetAllAsync();
            return Ok(playlists);
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> Get(int id)
        {
            var playlist = await _playlistRepo.GetByIdAsync(id);
            if (playlist == null) return NotFound();
            return Ok(playlist);
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlist>> Post([FromBody] Playlist playlist)
        {
            await _playlistRepo.AddAsync(playlist);
            return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
        }

        // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Playlist playlist)
        {
            if (id != playlist.Id) return BadRequest();
            await _playlistRepo.UpdateAsync(playlist);
            return NoContent();
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _playlistRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}