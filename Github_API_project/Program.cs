using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using DotNetEnv;

class OAuthApp
{
  // Remove hardcoded constants - we'll get these from environment variables
  private static string CLIENT_ID => Environment.GetEnvironmentVariable("GITHUB_CLIENT_ID");
  private static string CLIENT_SECRET => Environment.GetEnvironmentVariable("GITHUB_CLIENT_SECRET");
  private static string REDIRECT_URI => Environment.GetEnvironmentVariable("GITHUB_REDIRECT_URI");
  private static string AUTH_URL => Environment.GetEnvironmentVariable("GITHUB_AUTH_URL");
  private static string TOKEN_URL => Environment.GetEnvironmentVariable("GITHUB_TOKEN_URL");
  private static string API_BASE_URL => Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://api.github.com";
  static async Task Main(string[] args)
  {
    // Load environment variables from project root .env file
    DotNetEnv.Env.Load();
    // Validate required environment variables


    if (string.IsNullOrEmpty(CLIENT_ID))
            throw new Exception("GITHUB_CLIENT_ID is not set in environment variables");
    if (string.IsNullOrEmpty(CLIENT_SECRET))
      throw new Exception("GITHUB_CLIENT_SECRET is not set in environment variables");
    if (string.IsNullOrEmpty(REDIRECT_URI))
      throw new Exception("GITHUB_REDIRECT_URI is not set in environment variables");
    if (string.IsNullOrEmpty(AUTH_URL))
      throw new Exception("GITHUB_AUTH_URL is not set in environment variables");
    if (string.IsNullOrEmpty(TOKEN_URL))
      throw new Exception("GITHUB_TOKEN_URL is not set in environment variables");
    if (string.IsNullOrEmpty(API_BASE_URL))
      throw new Exception("API_BASE_URL is not set in environment variables");


    // Set up a simple web server to handle the callback
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();


    // The first Route Endpoint
    app.MapGet("/", (HttpContext context) =>
   {
     context.Response.ContentType = "text/html";
     var StatusCode = context.Response.StatusCode;
     return context.Response.WriteAsync("Welcome to Github App <a href='/login'>Login with Github</a>");
   });

    app.MapGet("/login", (HttpContext context) =>
    {
      const string scope = "";
      context.Response.ContentType = "text/html";


      var queryParameters = new
      {
        
      };

      var queryString = string.Join("&",
                 queryParameters.GetType().GetProperties().Select(prop => $"{prop.Name}={prop.GetValue(queryParameters)}"));

      var auth_url = $"{AUTH_URL}?{queryString}";
      context.Response.Redirect(auth_url);
      return Task.CompletedTask;
    });
    await app.RunAsync("");
  }
}