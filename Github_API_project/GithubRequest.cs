using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Github_API_project
{
    public class GithubRequest
    {
        private readonly string _username = "muwaffaqelbadawi";

        public async Task GithubRequestAsync()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            var response = await client.GetAsync($"https://api.github.com/users/{_username}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(result);

                if (jsonObject.TryGetProperty("created_at", out JsonElement createdAt))
                {
                    var createdAtStr = createdAt.GetString();
                    // var createdAtDateTime = DateTime.Parse(createdAtStr);

                    // Console.WriteLine($"User created at: {createdAtDateTime:yyyy-MM-dd HH:mm:ss}");
                    // Console.WriteLine($"Date: {createdAtDateTime:yyyy-MM-dd}");
                    // Console.WriteLine($"Time: {createdAtDateTime:HH:mm:ss}");
                }
                else
                {
                    Console.WriteLine("The 'created_at' property was not found in the response.");
                }
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }
    }
}