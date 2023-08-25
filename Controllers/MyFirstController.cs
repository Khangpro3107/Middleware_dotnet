using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Middleware.Controllers
{
    // this class will be used as a schema to read data from a req body
    public class Info
    {
        public string name { get; set; }
        public int age { get; set; }
        public Info()
        {
            age = 0;
            name = string.Empty;
            Console.WriteLine("Info ctor");
        }
    }
    public class MyFirstController : Controller
    {
        [Route("/first-get")]
        [HttpGet]
        public string Get()
        {
            return "Get";
        }

        // reading data from a req body
        [Route("/first-get")]
        [HttpPost]
        public string Post([FromBody] Info info)
        {
            Console.WriteLine("Post Controller");
            return info.name;
        }
    }
}
