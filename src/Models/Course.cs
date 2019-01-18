using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextCourses.Models
{

    /// <summary>
    /// This model represents the JSON output received from the courses endpoint
    /// </summary>
    public class UWCoursesJson
    {
        /// <summary>
        /// The metadata for the courses response
        /// </summary>
        public Meta meta { get; set; }
        /// <summary>
        /// The list of courses retrieved from the courses endpoint
        /// </summary>
        public List<UWCourse> data { get; set; }
    }

    /// <summary>
    /// This model represents the Course stored in the database, which is the Course retrieved from UWCoursesJson enriched with course_name netadata
    /// </summary>
    public class UWCourse
    {
        /// <summary>
        /// The combined subject and catalog_number of the course
        /// </summary>
        [Key]
        public string course_name { get; set; }
        /// <summary>
        /// The registrar-assigned course id
        /// </summary>
        public string course_id { get; set; }
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
    }
}