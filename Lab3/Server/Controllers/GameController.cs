using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Server.Controllers
{
    public class GameController : Controller
    {
        private DataService _data;
        public GameController(DataService data)
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
        public IActionResult Create(Game game)
        {
            _data.CreateGame(game);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var game = _data.GetGame(id);
            return View(game);
        }

        [HttpPost]
        public IActionResult Edit(Game game)
        {
            _data.UpdateGame(game);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _data.RemoveGame(id);
            return RedirectToAction("Index");
        }
    }
}
