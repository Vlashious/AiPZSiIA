using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server.Controllers
{
    public class CountryController : Controller
    {
        private DataService _data;
        public CountryController(DataService data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Country country)
        {
            _data.CreateCountry(country);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var country = _data.GetCountry(id);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country country)
        {
            _data.UpdateCountry(country);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _data.RemoveCountry(id);
            return RedirectToAction("Index");
        }
    }
}
