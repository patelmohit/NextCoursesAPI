using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NextCourses.Clients
{
    /// <summary>
    /// This class represents the client used to send requests to the UW Api
    /// </summary>
    public class UWClient
    {
        /// <summary>
        /// The HttpClient used to send requests to UW Api
        /// </summary>
        private readonly HttpClient _client = new HttpClient();
        /// <summary>
        /// The api key required to send requests
        /// </summary>
        private readonly string _apiKey;

        /// <summary>
        /// The constructor for the client
        /// </summary>
        /// <param name="apiKey"> The api key required to send requests to the UW Api </param>
        public UWClient(string apiKey)
        {
            _client.BaseAddress = new System.Uri("https://api.uwaterloo.ca/v2/");
            _apiKey = apiKey;
        }

        /// <summary>
        /// This method will send a request to the /courses endpoint and return the response
        /// </summary>
        public async Task<string> GetCourses()
        {
            var response = await _client.GetStringAsync($"courses.json?key={_apiKey}");
            return response;
        }

        /// <summary>
        /// This method will send a request to the /course/{subject}/{catalog_number}/prerequisites endpoint
        /// </summary>
        /// <param name="subject"> The subject of the course to search the prerequisite for </param>
        /// <param name="catalog_number"> The catalog number of the course to search the prerequisite for </param>
        public async Task<string> GetCoursePrerequisites(string subject, string catalog_number)
        {
            var response = await _client.GetStringAsync($"courses/{subject}/{catalog_number}/prerequisites.json?key={_apiKey}");
            return response;
        }
    }
}