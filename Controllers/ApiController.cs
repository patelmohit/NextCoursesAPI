using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace NextCourses.Controllers
{
    [Route("courses")]
    public class ApiController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<string>> Test()
        {
            return "Hello World";
        }
    }
}
