using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Main : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public string Say() => "Hello World!";

        [HttpGet]
        [Route("[action]")]
        public string Hello() => "Hello";
    }
}