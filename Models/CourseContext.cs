using Microsoft.EntityFrameworkCore;
using NextCourses.Models;
using System.Collections.Generic;

namespace NextCourses.Context
{
    /// <summary>
    /// DbContext used for storing course related information
    /// </summary>
    public class CourseContext : DbContext
    {
        /// <summary>
        /// Database table containing course metadata
        /// </summary>
        public DbSet<UWCourse> Courses { get; set; }
        /// <summary>
        /// Database table containing prerequisite information
        /// </summary>
        public DbSet<PrereqMap> Prereqs { get; set; }

        /// <summary>
        /// Configure sqlite database to be created locally as courses.db
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=courses.db");
        }

        /// <summary>
        /// Use a primary key consisting of multiple fields with both prereq course name and next course name
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrereqMap>().HasKey(p => new { p.prereq_course_name, p.next_course_name});
        }
    }
}