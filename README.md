[API Link]: https://nextcoursesapi.azurewebsites.net/swagger/index.html

# NextCoursesAPI
[![Build Status](https://travis-ci.org/patelmohit/NextCoursesAPI.svg?branch=master)](https://travis-ci.org/patelmohit/NextCoursesAPI)

NextCoursesAPI is an API which will help you select which University of Waterloo courses you can take next. [Link to Swagger UI.][API Link]

## Motivation

Searching for courses to take in university can be a tricky endeavor. The courses you need to take can be difficult to plan out, especially if they have a long sequence of prerequisites. You might also be interested in finding out if any of the courses you have taken satisfy the prerequisites of courses you never heard about.

NextCoursesAPI seeks to solve this problem by allowing you to search for all of the courses that have your course as a prerequisite. You can start with a first year course that you took in the 1A term, and see all of the courses that require that first year course as a prerequisite. If you repeat this for all of your 1A courses, you will have a diverse list of courses that you might be able to take in your 1B term, which will help you with your course selections. The possibilities of courses you can take at UW are endless, and this tool will help you plan it out.

## How does it work?

NextCoursesAPI is written in C#, and uses ASP.NET Core 2.2 for the Web API. It keeps track of all courses and their prerequisites in a SQLite database, which is manipulated through Entity Framework Core. All information about University of Waterloo Courses is retrieved through the [UW Open Data Api](https://uwaterloo.ca/api/). The database is built by first retrieving a list of all courses from the [`/courses`](https://github.com/uWaterloo/api-documentation/blob/master/v2/courses/courses.md) endpoint, and then maintaining a mapping of their prerequisites by navigating the [`/courses/{subject}/{catalog_number}/prerequisites`](https://github.com/uWaterloo/api-documentation/blob/master/v2/courses/subject_catalog_number_prerequisites.md) endpoint for each course. 

Travis CI builds and tests the code. The Web API is currently running as an Azure Web App.

Logs are created through Serilog, and a folder `src/Logs` will be created which will contain the logs.

To assist with the use of this API, the API has been developed following the OpenAPI Specification. You can test the API from the [Swagger UI][API Link] without having to create your own request.

## Sample Response
<details>
    <summary>API response for PSYCH 257</summary>


Request:

```bash
curl -X GET "https://nextcoursesapi.azurewebsites.net/courses/PSYCH/257" -H  "accept: text/plain"
```

Response:
```json
{
  "prerequisite_course": "PSYCH257",
  "next_courses": [
    {
      "course_name": "Human Motivation and Emotion",
      "course_title": "PSYCH332"
    },
    {
      "course_name": "Introduction to Clinical Psychology",
      "course_title": "PSYCH336"
    },
    {
      "course_name": "Social Science Advanced Research Methods Topics",
      "course_title": "PSYCH389"
    },
    {
      "course_name": "Research in Personality and Clinical Psychology",
      "course_title": "PSYCH397"
    },
    {
      "course_name": "Honours Seminar in Personality and Clinical Psychology",
      "course_title": "PSYCH457"
    },
    {
      "course_name": "Special Topics in Applied Psychology",
      "course_title": "PSYCH470"
    }
  ]
}
```
</details>

## Deploying
Using the .NET Core CLI from the root directory, type the following commands:
```bash
cd src
dotnet run
```

## Testing
You can run the NUnit tests by using the .NET Core CLI and typing the following command from the root directory:
```bash
cd tests
dotnet test
```
## Update frequency
The database and web API will be updated each term, since the list of courses is updated each term. There is a helper class in `src/ 	Maintenance/BuildDatabases.cs` which will assist with the creation of the SQLite database. You will need a valid UW Api Key in order to use the helper class.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

* [University of Waterloo Open Data API](https://github.com/uWaterloo/api-documentation) 
