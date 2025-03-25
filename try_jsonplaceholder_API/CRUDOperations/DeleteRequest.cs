using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace try_jsonplaceholder_API.CRUDOperations
{
    public class DeleteRequest
    {
        private readonly int id = 5;
        public async Task DeleteRequestAsync()
        {
            using var client = new HttpClient();


            var response = await client.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        }
    }
}