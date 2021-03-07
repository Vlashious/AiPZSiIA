using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IService<Publisher> _service;
        public PublisherController(PublisherService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Publisher>> Get() => _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetPublisher")]
        public ActionResult<Publisher> Get(string id)
        {
            var publisher = _service.Get(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        [HttpPost]
        public ActionResult<Publisher> Create([FromBody] Publisher publisher)
        {
            _service.Create(publisher);

            return CreatedAtRoute("GetPublisher", new { id = publisher.Id.ToString() }, publisher);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Publisher publisherIn)
        {
            var publisher = _service.Get(id);

            if (publisher == null)
            {
                return NotFound();
            }

            _service.Update(id, publisherIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var publisher = _service.Get(id);

            if (publisher == null)
            {
                return NotFound();
            }

            _service.Remove(id);

            return NoContent();
        }
    }
}