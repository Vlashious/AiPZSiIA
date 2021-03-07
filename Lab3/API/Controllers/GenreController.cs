using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IService<Genre> _service;
        public GenreController(GenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Genre>> Get() => _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetGenre")]
        public ActionResult<Genre> Get(string id)
        {
            var genre = _service.Get(id);
            if(genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public ActionResult<Genre> Create([FromBody] Genre genre)
        {
            _service.Create(genre);

            return CreatedAtRoute("GetGenre", new { id = genre.Id.ToString() }, genre);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Genre genreIn)
        {
            var genre = _service.Get(id);

            if(genre == null)
            {
                return NotFound();
            }

            _service.Update(id, genreIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var genre = _service.Get(id);

            if(genre == null)
            {
                return NotFound();
            }

            _service.Remove(id);

            return NoContent();
        }
    }
}