using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace try_jsonplaceholder_API.CRUDOperations
{
    public class PatchRequest
    {
        private readonly string _id = "1";
        public async Task PatchRequestAsync()
        {
            var update = new
            {
                title = "foo"
            };

            var json = JsonSerializer.Serialize(update);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PatchAsync($"https://jsonplaceholder.typicode.com/posts/{_id}", data);

            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonElement>(result);
            Console.WriteLine(jsonObject);

        }
    }

    // Extension method for HttpClient to support PATCH method
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri(requestUri),
                Content = content
            };

            return await client.SendAsync(request);
        }
    }
}