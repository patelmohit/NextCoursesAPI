using Newtonsoft.Json;
using NextCourses.Maintenence;
using NextCourses.Models;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        DatabaseBuilder _dbBuilder;
        [SetUp]
        public void Setup()
        {
            _dbBuilder = new DatabaseBuilder("test-key", "test-connection-string");
        }

        [Test]
        public void ParsePHYS375()
        {
            PrereqJson prereq = JsonConvert.DeserializeObject<PrereqJson>(File.ReadAllText("PHYS375.json"));
            HashSet<string> prereqSet = new HashSet<string>();
            _dbBuilder.ParsePrerequisites(ref prereqSet, prereq.data.prerequisites_parsed);
            string[] expectedCourses = new string[] {
                "PHYS112",
                "PHYS122",
                "PHYS234",
                "PHYS242",
                "PHYS256",
                "PHYS275",
                "PHYS358",
                "AMATH271"
            };
            foreach (string course in expectedCourses)
            {
                if (!prereqSet.Contains(course)) 
                {
                    Assert.Fail($"Failed to parse {course} as a prerequisite for PHYS375");
                }
            }
            Assert.Pass("Parsed all prerequisites for PHYS375");
        }
    }
}