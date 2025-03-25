using System;
using System.Threading;
using System.Threading.Tasks;
using Github_API_project;



namespace Github_API_project
{
  class Program
  {
    static async Task Main()
    {
      var githubRequest = new GithubRequest();
      await githubRequest.GithubRequestAsync();
    }
  }
}