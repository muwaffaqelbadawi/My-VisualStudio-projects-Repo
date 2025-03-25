using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace try_jsonplaceholder_API.CRUDOperations
{
    public class PutRequest
    {
        public async Task PutRequestAsync()
        {
            var post = new
            {
                id = 10,
                title = "car",
                body = "sports car",
                userId = 10
            };

            int id = post.id;
            var json = JsonSerializer.Serialize(post);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync($"https://jsonplaceholder.typicode.com/posts/{id}", data);

            var result = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonElement>(result);
            Console.WriteLine(jsonObject);
        }
    }
}