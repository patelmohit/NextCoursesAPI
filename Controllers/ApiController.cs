using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NextCourses.Clients;
using NextCourses.Context;
using NextCourses.Models;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NextCourses.Controllers
{
    [Route("courses")]
    public class ApiController : Controller
    {
        private IConfiguration _configuration {get; set;}
        private UWClient _uwClient;
        private readonly CourseContext _context;

        public ApiController(IConfiguration configuration, UWClient uwClient, CourseContext context)
        {
            _configuration = configuration;
            _uwClient = uwClient;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Test()
        {
            var result = await _uwClient.GetCourses(_configuration["UWApiKey"]);
            UWCourseJson course = JsonConvert.DeserializeObject<UWCourseJson>(result);
            _context.Courses.Add(course.data[0]);
            _context.SaveChanges();
            return _context.Courses.First().title;
            //return course.data[0].subject + " " + course.data[0].title;

        }
    }
}
