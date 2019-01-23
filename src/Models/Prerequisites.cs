using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCourses.Models
{

    /// <summary>
    /// This model represents the JSON output received from the prerequisites endpoint
    /// </summary>
    public class PrereqJson
    {
        /// <summary>
        /// The metadata for the prerequisite response
        /// </summary>
        public Meta meta { get; set; }

        /// <summary>
        /// The prerequisites-related information from the prerequisites response
        /// </summary>
        public PrereqInfo data { get; set; }
    }

    /// <summary>
    /// This class represents the prerequisite information obtained from the prerequisite endpoint
    /// </summary>
    public class PrereqInfo
    {
        /// <summary>
        /// The subject of the course
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// The catalog number of the course
        /// </summary>
        public string catalog_number { get; set; }
        /// <summary>
        /// The title of the course
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// The user-friendly prerequisites of the course
        /// </summary>
        public string prerequisites { get; set; }
        /// <summary>
        /// The parsed prerequisites of the course, serialized as objects
        /// </summary>
        public List<object> prerequisites_parsed { get; set; }
    }

    /// <summary>
    /// This class represents a mapping between a course and its prerequisite
    /// </summary>
    public class PrereqMap
    {
        /// <summary>
        /// The course name of the prerequisite
        /// </summary>
        public string prereq_course_name { get; set; }
        /// <summary>
        /// The course name of the course taken after the prerequisite
        /// </summary>
        public string next_course_name { get; set; }
    }

    /// <summary>
    /// The next courses that can be taken after a given course. Includes the name of the course and a list of NextCourseInfo for the courses that can be taken after.
    /// </summary>
    public class NextCourseResponse
    {
        /// <summary>
        /// Constructor for response
        /// </summary>
        public NextCourseResponse(string course_name, List<NextCourseInfo> course_list)
        {
            prerequisite_course = course_name;
            next_courses = course_list;
        }
        /// <summary>
        /// The name of the prerequisite course.
        /// </summary>
        public string prerequisite_course { get; set; }
        /// <summary>
        /// The list of courses that can be taken after the prerequisite course.
        /// </summary>
        public List<NextCourseInfo> next_courses { get; set; }
    }

    /// <summary>
    /// This class represents useful information about the next course.
    /// </summary>
    public class NextCourseInfo
    {
        /// <summary>
        /// Constructor for NextCourseInfo
        /// </summary>
        public NextCourseInfo (string name, string title)
        {
            course_name = name;
            course_title = title;
        }
        /// <summary>
        /// Name of the next course.
        /// </summary>
        public string course_name { get; set; }
        /// <summary>
        /// Title of the next course.
        /// </summary>
        public string course_title { get; set; }
    }

}