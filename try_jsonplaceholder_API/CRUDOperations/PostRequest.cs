using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace try_jsonplaceholder_API.CRUDOperations
{
    public class PostRequest
    {
        public async Task PostRequestAsync()
        {
            var post = new
            {
                title = "foo",
                body = "bar",
                userId = 1
            };

            var json = JsonSerializer.Serialize(post);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync("https://jsonplaceholder.typicode.com/posts", data);

            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonElement>(result);
            Console.WriteLine(jsonObject);
        }
    }
}