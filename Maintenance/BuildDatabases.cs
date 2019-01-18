using Newtonsoft.Json;
using NextCourses.Clients;
using NextCourses.Context;
using NextCourses.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NextCourses.Maintenence
{
    /// <summary>
    /// Class which is responsible for creating the database used by the API.
    /// </summary>
    class BuildDatabases
    {
        /// <summary>
        /// HTTP client to connect to the UWaterloo API
        /// </summary>
        static UWClient _client;
        /// <summary>
        /// The DbContext which stores course-related information
        /// </summary>
        static CourseContext _context;
        /// <summary>
        /// This method will add all necessary course-related information to the database. It accomplishes this by first retrieving all courses from the /courses endpoint, and iterates through each course to find all possible prerequisite course pairs. This information is then stored in the database in the Courses and Prereqs tables.
        /// </summary>
        /// <param name="apiKey"> The UW Api Key used to send requests </param>
        /// <returns> Returns a database with course-related information and prerequsite-related pairs. </returns>
        public static async Task Build(string apiKey)
        {
            _client = new UWClient(apiKey);
            _context = new CourseContext();

            var coursesResult = await _client.GetCourses();
            UWCoursesJson allCourses = JsonConvert.DeserializeObject<UWCoursesJson>(coursesResult);
            var apiResults = allCourses.data.Select(course => SearchPrereqs(course));
            var prereqOutput = await Task.WhenAll(apiResults);

            foreach (var prereqTuple in prereqOutput)
            {
                try
                {
                    _context.Courses.Add(prereqTuple.Item1);
                    foreach (var prereqSet in prereqTuple.Item2)
                    {
                        _context.Prereqs.Add(prereqSet);
                    }
                    _context.SaveChanges();
                } 
                catch (Exception e)
                {
                    Log.Error("Caught exception while saving prereqs to database: " + prereqTuple.ToString() + " : " + e.Message);
                }
            }
        }
        
        /// <summary>
        /// This method will send a request to the prerequisite endpoint and return the corresponding prerequisites for the given course
        /// </summary>
        /// <param name="course"> The course to search the prerequisites for</param>
        /// <returns>Returns a tuple containing the course and the list of corresponding prerequisites </returns>
        private static async Task<Tuple<UWCourse, List<PrereqMap>>> SearchPrereqs(UWCourse course)
        {
            try
            {
                string prereqResults = "";
                List<PrereqMap> prereqMap = new List<PrereqMap>();
                Log.Information("Searching prerequisites for " + course.subject + course.catalog_number);
                try
                {
                    prereqResults = await _client.GetCoursePrerequisites(course.subject, course.catalog_number);  
                }
                catch
                {
                    Log.Error("Error in HTTP request for SearchPrereqs for " + course.subject + course.catalog_number);
                    return new Tuple<UWCourse, List<PrereqMap>>(course, prereqMap);
                }
                course.course_name = course.subject + course.catalog_number;
                List<object> prereqList = JsonConvert.DeserializeObject<PrereqJson>(prereqResults).data.prerequisites_parsed;
                HashSet<string> prereqSet = new HashSet<string>();
                ParsePrerequisites(ref prereqSet, prereqList);
                foreach (string s in prereqSet)
                {
                    PrereqMap dbInput = new PrereqMap();
                    dbInput.prereq_course_name = s;
                    dbInput.next_course_name = course.course_name;
                    prereqMap.Add(dbInput);
                }
                Log.Information("Done searching prerequisites for " + course.subject + course.catalog_number);
                return new Tuple<UWCourse, List<PrereqMap>>(course, prereqMap);
            }
            catch (Exception e)
            {
                Log.Error("Caught exception for course " + course.ToString() + " with message: " + e.Message);
                return new Tuple<UWCourse, List<PrereqMap>>(course, new List<PrereqMap>());
            }
        }

        /// <summary>
        /// This method will parse the prerequisite_parsed response from the prerequisite endpoint, and add those prerequisites to prereqSet
        /// </summary>
        /// <param name="prereqSet"> The set of prerequisites for the course </param>
        /// <param name="prereqJson"> The prerequisites_parsed JSON to be parsed </param>
        /// <returns> Updates the prereqSet with all prerequisites parsed </returns>
        private static void ParsePrerequisites(ref HashSet<string> prereqSet, List<object> prereqJson)
        {
            if (prereqJson == null || !prereqJson.Any()) return;
            else
            {
                for (var i = 0; i < prereqJson.Count; ++i)
                {
                    object current = prereqJson[i];
                    if (current is string)
                    {
                        prereqSet.Add(current as string);
                    }
                    else if (current is Newtonsoft.Json.Linq.JArray)
                    {
                        Newtonsoft.Json.Linq.JArray casted = (Newtonsoft.Json.Linq.JArray)current;
                        ParsePrerequisites(ref prereqSet, casted.ToObject<List<object>>());
                    }
                }
            }
        }
    }
}