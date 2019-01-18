using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NextCourses.Clients;
using NextCourses.Context;
using NextCourses.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextCourses.Controllers
{
    /// <summary>
    /// Contoller to redirect the user from the root address
    /// </summary>
    [Route("")]
    public class SwaggerRedirectController : Controller
    {
        /// <summary>
        /// Redirects request from root address to Swagger Ui
        /// </summary>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/index.html");
        }
    }

    /// <summary>
    /// Controller which will return the next courses a user can take for a given course
    /// </summary>
    [Route("courses")]
    public class NextCoursesController : Controller
    {
        /// <summary>
        /// The DbContext which stores course-related information
        /// </summary>
        private readonly CourseContext _context;

        /// <summary>
        /// Constructor for controller which uses Dependency Injection to store the DbContext
        /// </summary>
        /// <param name="context">The DbContext to be used to retrieve course-related information </param>
        public NextCoursesController(CourseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the list of courses that have the given course as a prerequisite.
        /// </summary>
        /// <param name="subject">The subject of the course. Example: CS</param>
        /// <param name="catalog_number"> The catalog number of the course. Example: 350</param>
        /// <returns> Returns a NextCourseReponse which contains the list of next courses </returns>
        [HttpGet("{subject}/{catalog_number}")]
        [ProducesResponseType(200, Type = typeof(NextCourseReponse))]
        [ProducesResponseType(404)]
        public ActionResult<NextCourseReponse> Get(string subject, string catalog_number)
        {
            Log.Information($"Searching next courses for {subject} {catalog_number}");
            string course_name = subject.ToUpper() + catalog_number;
            if (_context.Courses.FirstOrDefault(course => course.course_name == course_name) == null)
            {
                Log.Warning($"Course {course_name} not found");
                return NotFound();
            }
            List<NextCourseInfo> next_courses = new List<NextCourseInfo>();
            List<PrereqMap> prereqs =  _context.Prereqs.Where(
                                        course => course.prereq_course_name == course_name).ToList();
            prereqs.ForEach(prereqMap =>
            {
                string next_course_title = _context.Courses.FirstOrDefault(
                    course => course.course_name == prereqMap.next_course_name).title;
                next_courses.Add(new NextCourseInfo(next_course_title, prereqMap.next_course_name));
            });
            next_courses.OrderBy(course => course.course_name);
            NextCourseReponse output = new NextCourseReponse(course_name, next_courses);
            return output;
        }
    }
}
