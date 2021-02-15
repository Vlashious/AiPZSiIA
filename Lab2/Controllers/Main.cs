using Database;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Bson;

namespace Controllers
{
    public class MainController : Controller
    {
        private Data _data;

        public MainController(Data data)
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
            _data.InsertData(game);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var game = _data.GetGame(ObjectId.Parse(id));
            return View(game);
        }

        [HttpPost]
        public IActionResult Edit(Game game)
        {
            game.Id = ObjectId.Parse(game.StringId);
            _data.UpdateGame(game.Id, game);
            return Redirect("/");
        }

        public IActionResult Remove(string id)
        {
            _data.RemoveGame(ObjectId.Parse(id));
            return Redirect("/");
        }
    }
}