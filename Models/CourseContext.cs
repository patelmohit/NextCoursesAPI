using Microsoft.EntityFrameworkCore;
using NextCourses.Models;
using System.Collections.Generic;

namespace NextCourses.Context
{
    public class CourseContext : DbContext
    {
        public DbSet<UWCourse> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=courses.db");
        }
    }
}