using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Server.Controllers
{
    public class GenreController : Controller
    {
        private DataService _data;
        public GenreController(DataService data)
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
        public IActionResult Create(Genre genre)
        {
            _data.CreateGenre(genre);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var genre = _data.GetGenre(id);
            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            _data.UpdateGenre(genre);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _data.RemoveGenre(id);
            return RedirectToAction("Index");
        }
    }
}
