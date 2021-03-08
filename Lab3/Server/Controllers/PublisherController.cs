using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server.Controllers
{
    public class PublisherController : Controller
    {
        private DataService _data;
        public PublisherController(DataService data)
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
        public IActionResult Create(Publisher publisher)
        {
            _data.CreatePublisher(publisher);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var country = _data.GetPublisher(id);
            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            _data.UpdatePublisher(publisher);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _data.RemovePublisher(id);
            return RedirectToAction("Index");
        }
    }
}
