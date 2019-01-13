using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key]
        public string course_id { get; set; }
        public string subject { get; set; }
        public string catalog_number { get; set; }
        public string title { get; set; }
        public double units { get; set; }
        public string description { get; set; }
        [NotMapped]
        public List<string> instructions { get; set; }
        public string prerequisites { get; set; }
        public string antirequisites { get; set; }
        public string corequisites { get; set; }
        public string crosslistings { get; set; }
        [NotMapped]
        public List<string> terms_offered { get; set; }
        public string notes { get; set; }
        [NotMapped]
        public Offerings offerings { get; set; }
        public Boolean needs_department_consent { get; set; }
        public Boolean needs_instructor_consent { get; set; }
        [NotMapped]
        public List<string> extra { get; set; }
        public string calendar_year { get; set; }
        public string url { get; set; }
        public string academic_level { get; set; }
    }

    public class Offerings
    {
        public Boolean online { get; set; }
        public Boolean online_only { get; set; }
        public Boolean st_jerome { get; set; }
        public Boolean st_jerome_only { get; set; }
        public Boolean renison { get; set; }
        public Boolean renison_only { get; set; }
        public Boolean conrad_grebel { get; set; }
        public Boolean conrad_grebel_only { get; set; }
    }

}