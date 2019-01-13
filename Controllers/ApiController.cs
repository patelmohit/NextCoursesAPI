using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NextCourses.Clients;
using NextCourses.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace NextCourses.Controllers
{
    [Route("courses")]
    public class ApiController : Controller
    {
        private IConfiguration _configuration {get; set;}
        private UWClient _uwClient;

        public ApiController(IConfiguration configuration, UWClient uwClient)
        {
            _configuration = configuration;
            _uwClient = uwClient;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Test()
        {
            var result = await _uwClient.GetCourses(_configuration["UWApiKey"]);
            UWCourseJson course = JsonConvert.DeserializeObject<UWCourseJson>(result);
            return course.data[0].subject + " " + course.data[0].title;
        }
    }
}
