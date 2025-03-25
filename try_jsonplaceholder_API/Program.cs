using System;
using System.Threading;
using System.Threading.Tasks;
using try_jsonplaceholder_API.CRUDOperations;


namespace try_jsonplaceholder_API
{
  class Program
  {
    static async Task Main()
    {
      var getRequest = new GetRequest();
      // var postRequest = new PostRequest();
      // var putRequest = new PutRequest();
      // var patchRequest = new PatchRequest();
      // var deleteRequest = new DeleteRequest();

      await getRequest.GetRequestAsync();
      // await postRequest.PostRequestAsync();
      // await putRequest.PutRequestAsync();
      // await patchRequest.PatchRequestAsync();
      // await deleteRequest.DeleteRequestAsync();
    }
  }
}