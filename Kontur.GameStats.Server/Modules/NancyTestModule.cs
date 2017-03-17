using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace Kontur.GameStats.Server.Modules
{
  public class NancyTestModule : NancyModule
  {
    public class JsonModel
    {
      public string stringField { get; set; }
      public int intField { get; set; }
    }

    public NancyTestModule()
    {
      //Get["/ex"] = _ => { throw new NotFoundErrorException();};
      Get["/get_json"] = _
          => Response.AsJson(new JsonModel { intField = 5, stringField = "fgds" });
      Post["/post_json"] = _ =>
      {
        var model = this.Bind<JsonModel>();
        return $"Get model: stringField={model.stringField}, intField={model.intField}";
      };
      Get["/nancy/{index}", true] = async (x, ct) =>
      {
        var res = await DoingWorkInThread(x.index);
        return res;
      };
      Get["/nancy"] = _ => "Hello, Nancy is working.";

      Get["/dynamicDict"] = _ =>
       {
         var dict = new Dictionary<string, dynamic>
         {
           {"int_val", 3},
           {"double_val", 3.14},
           {"datetime_val", DateTime.Now.ToUniversalTime()},
           {"array_val", new[] {"str1", "str2"}}
         };
         return Response.AsJson(dict);
       };
    }

    private Task<HttpStatusCode> DoingWorkInThread(int index)
    {
      var task = new Task<HttpStatusCode>(n =>
      {
        try
        {
          Foo((int)n);
        }
        catch (Exception e)
        {
          return HttpStatusCode.InternalServerError;
        }
        return HttpStatusCode.OK;
      }, index);
      task.Start();
      return task;
    }

    private string Foo(int n)
    {
      Console.WriteLine($"Task #{n} is started in {Thread.CurrentThread.ManagedThreadId} thread.");
      var rnd = new Random();
      Thread.Sleep(rnd.Next(10, 200));
      //throw new Exception();
      Console.WriteLine($"Task #{n} ended in {Thread.CurrentThread.ManagedThreadId} thread.");
      return $"Task #{n} ended in {Thread.CurrentThread.ManagedThreadId} thread.";
    }
  }
}