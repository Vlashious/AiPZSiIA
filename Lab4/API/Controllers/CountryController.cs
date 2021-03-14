using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IService<Country> _service;
        public CountryController(CountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Country>> Get() => _service.Get();

        [HttpGet("{id:length(24)}", Name = "GetCountry")]
        public ActionResult<Country> Get(string id)
        {
            var country = _service.Get(id);
            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        [HttpPost]
        public ActionResult<Country> Create([FromBody] Country country)
        {
            _service.Create(country);

            return CreatedAtRoute("GetCountry", new { id = country.Id.ToString() }, country);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Country countryIn)
        {
            var country = _service.Get(countryIn.Id);

            if (country == null)
            {
                return NotFound();
            }

            _service.Update(countryIn.Id, countryIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var country = _service.Get(id);

            if (country == null)
            {
                return NotFound();
            }

            _service.Remove(id);

            return NoContent();
        }
    }
}