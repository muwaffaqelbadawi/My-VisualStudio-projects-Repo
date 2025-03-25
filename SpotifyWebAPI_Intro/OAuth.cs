using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

class OAuthApp
{
  private const string CLIENT_ID = "YOUR_CLIENT_ID";
  private const string CLIENT_SECRET = "YOUR_CLIENT_SECRET";
  private const string REDIRECT_URI = "http://localhost:5543/callback";
  private const string AUTH_URL = "https://accounts.spotify.com/authorize";
  private const string TOKEN_URL = "https://accounts.spotify.com/api/token";
  private const string API_BASE_URL = "https://api.spotify.com/v1/";

  static async Task Main(string[] args)
  {
    // Make sure "http://localhost:5543/callback" is in your application's redirect URIs!
    // Set up a simple web server to handle the callback
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();


    // The first Route Endpoint
    app.MapGet("/", (HttpContext context) =>
   {
     context.Response.ContentType = "text/html";
     var StatusCode = context.Response.StatusCode;
     return context.Response.WriteAsync("Welcome to Spotify App <a href='/login'>Login with Spotify</a>");
   });

    app.MapGet("/login", (HttpContext context) =>
    {
      const string scope = "user-read-private user-read-email";

      var queryParameters = new
      {
        client_id = CLIENT_ID,
        response_type = "code",
        scope = scope,
        redirect_uri = REDIRECT_URI,
        // show_dialog = true,
      };

      var queryString = string.Join("&",
                 queryParameters.GetType().GetProperties().Select(prop => $"{prop.Name}={prop.GetValue(queryParameters)}"));

      var auth_url = $"{AUTH_URL}?{queryString}";
      context.Response.Redirect(auth_url);
      return Task.CompletedTask;
    });
    await app.RunAsync("http://localhost:5543");
  }
}