using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloWorld : ControllerBase
    {
        [HttpGet]
        [Route("api/[controller]/say")]
        public string Say() => "Hello World!";

        [HttpGet]
        [Route("api/[controller]/hello")]
        public string Hello() => "Hello";
    }
}