using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace try_jsonplaceholder_API.CRUDOperations
{
    public class GetRequest
    {
        public async Task GetRequestAsync()
        {
            using var client = new HttpClient();

            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/5");

            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonElement>(result);
            Console.WriteLine(jsonObject);
        }
    }
}