# Day 015 - Microsoft C# Task.All(IEnumerable<Task>)

  ### What is C# Task.WhenAll<>?
  WhenAll(IEnumerable<Task>) Creates a task that will complete when all of the Task objects in an enumerable collection have completed. WhenAll(Task[]) Creates a task that will complete when all of the Task objects in an array have completed.

  ### Range
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Net.NetworkInformation;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        public class Program
        {
            public static async Task Main()
            {
                int failed = 0;
                var tasks = new List<Task>();
                String[] urls = { "www.adatum.com", "www.cohovineyard.com",
                            "www.cohowinery.com", "www.northwindtraders.com",
                            "www.contoso.com" };

                foreach (var value in urls)
                {
                    var url = value;
                    tasks.Add(Task.Run(() => {
                        var png = new Ping();
                        try
                        {
                            var reply = png.Send(url);
                            if (!(reply.Status == IPStatus.Success))
                            {
                                Interlocked.Increment(ref failed);
                                throw new TimeoutException("Unable to reach " + url + ".");
                            }
                        }
                        catch (PingException)
                        {
                            Interlocked.Increment(ref failed);
                            throw;
                        }
                    }));
                }
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }

                if (t.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("All ping attempts succeeded.");
                else if (t.Status == TaskStatus.Faulted)
                    Console.WriteLine("{0} ping attempts failed", failed);
            }
        }
    }
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenall?view=net-5.0