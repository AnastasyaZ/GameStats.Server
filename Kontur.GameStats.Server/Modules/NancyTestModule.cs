using System;
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
            //Get["/"] = _ => "Hello, Nancy is working.";
            Get["/get_json"] = _
                => Response.AsJson(new JsonModel() { intField = 5, stringField = "fgds" });
            Post["/post_json"] = _ =>
            {
                var model = this.Bind<JsonModel>();
                return $"Get model: stringField={model.stringField}, intField={model.intField}";
            };
            Get["/nancy/{index}", true] = async (x, ct) => { await DoingWorkInThread(x.index); return HttpStatusCode.OK; };
            Get["/nancy"] = _ => "Hello, Nancy is working.";
        }

        Task<string> DoingWorkInThread(string index)
        {
            var task = new Task<string>((n) =>
            {
                Console.WriteLine($"Task #{n} is started in {Thread.CurrentThread.ManagedThreadId} thread.");
                var rnd = new Random();
                Thread.Sleep(rnd.Next(10, 200));
                Console.WriteLine($"Task #{n} ended in {Thread.CurrentThread.ManagedThreadId} thread.");
                return $"Task #{n} ended in {Thread.CurrentThread.ManagedThreadId} thread.";
            }, index);
            task.Start();
            return task;
        }
    }
}