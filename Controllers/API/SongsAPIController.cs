namespace CSCI3110_TermProject.Controllers.API
{
    using CSCI3110_TermProject.Models;
    using CSCI3110_TermProject.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class SongsApiController : ControllerBase
    {
        private readonly ISongRepository _songRepo;

        public SongsApiController(ISongRepository songRepo)
        {
            _songRepo = songRepo;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> Get()
        {
            var songs = await _songRepo.GetAllAsync();
            return Ok(songs);
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> Get(int id)
        {
            var song = await _songRepo.GetByIdAsync(id);
            if (song == null) return NotFound();
            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> Post([FromBody] Song song)
        {
            await _songRepo.AddAsync(song);
            return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song song)
        {
            if (id != song.Id) return BadRequest();
            await _songRepo.UpdateAsync(song);
            return NoContent();
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _songRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
