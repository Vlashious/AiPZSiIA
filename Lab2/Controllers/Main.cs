using Database;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Edit(string id)
        {
            var game = _data.GetGame(ObjectId.Parse(id));
            return View(game);
        }

        public IActionResult Remove(string id)
        {
            return Redirect("/");
        }
    }
}