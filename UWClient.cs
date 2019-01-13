using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NextCourses.Clients
{
    public class UWClient
    {
        private readonly HttpClient Client;
        public UWClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<string> GetCourses(string apiKey)
        {
            var response = await Client.GetStringAsync("courses.json?key=" + apiKey);
            return response;
        }
    }
}