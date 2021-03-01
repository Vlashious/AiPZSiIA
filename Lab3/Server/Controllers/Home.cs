using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    public class Home : Controller
    {
        private DataService _data;
        public Home(DataService data)
        {
            _data = data;
        }
        public string Index()
        {
            return _data.MakeRequest();
        }
    }
}