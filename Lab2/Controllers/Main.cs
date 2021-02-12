using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}