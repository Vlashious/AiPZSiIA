using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly BaseService<Game> _service;
        public GameController(GameService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Game>> Get() => _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<Game> Get(string id)
        {
            var game = _service.Get(id);
            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public ActionResult<Game> Create([FromBody] Game game)
        {
            _service.Create(game);

            return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Game gameIn)
        {
            var publisher = _service.Get(gameIn.Id);

            if (publisher == null)
            {
                return NotFound();
            }

            _service.Update(gameIn.Id, gameIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var game = _service.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            _service.Remove(id);

            return NoContent();
        }
    }
}