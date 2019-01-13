using System;
using System.Collections.Generic;
namespace NextCourses.Models
{

    public class UWCourseJson
    {
        public Meta meta { get; set; }
        public List<UWCourse> data { get; set; }
    }

    public class Meta
    {
        public int requests;
        public int timestamp;
        public int status;
        public string message;
        public int method_id;

    }

    public class UWCourse
    {
        public string course_id { get; set; }
        public string subject { get; set; }
        public string catalog_number { get; set; }
        public string title { get; set; }
    }
}