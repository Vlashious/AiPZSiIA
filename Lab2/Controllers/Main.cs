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
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreatePublisher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateGame(Game game)
        {
            _data.InsertData(game);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult CreateGenre(Genre genre)
        {
            _data.InsertData(genre);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult CreateCountry(Country country)
        {
            _data.InsertData(country);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult CreatePublisher(Publisher publisher)
        {
            _data.InsertData(publisher);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult EditGame(string id)
        {
            var game = _data.GetGame(ObjectId.Parse(id));
            return View(game);
        }

        [HttpGet]
        public IActionResult EditCountry(string id)
        {
            var country = _data.GetCountry(ObjectId.Parse(id));
            return View(country);
        }

        [HttpGet]
        public IActionResult EditGenre(string id)
        {
            var genre = _data.GetGenre(ObjectId.Parse(id));
            return View(genre);
        }

        [HttpGet]
        public IActionResult EditPublisher(string id)
        {
            var publisher = _data.GetPublisher(ObjectId.Parse(id));
            return View(publisher);
        }

        [HttpPost]
        public IActionResult EditGame(Game game)
        {
            game.Id = ObjectId.Parse(game._innderId);
            _data.UpdateGame(game.Id, game);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult EditCountry(Country country)
        {
            country.Id = ObjectId.Parse(country._innderId);
            _data.UpdateCountry(country.Id, country);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            genre.Id = ObjectId.Parse(genre._innderId);
            _data.UpdateGenre(genre.Id, genre);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult EditPublisher(Publisher publisher)
        {
            publisher.Id = ObjectId.Parse(publisher._innderId);
            _data.UpdatePublisher(publisher.Id, publisher);
            return Redirect("/");
        }

        public IActionResult RemoveGame(string id)
        {
            _data.RemoveGame(ObjectId.Parse(id));
            return Redirect("/");
        }

        public IActionResult RemoveCountry(string id)
        {
            _data.RemoveCountry(ObjectId.Parse(id));
            return Redirect("/");
        }

        public IActionResult RemoveGenre(string id)
        {
            _data.RemoveGenre(ObjectId.Parse(id));
            return Redirect("/");
        }

        public IActionResult RemovePublisher(string id)
        {
            _data.RemovePublisher(ObjectId.Parse(id));
            return Redirect("/");
        }
    }
}