# Day 016 - Microsoft C# Cancellation Token

  ### What is C# Task.WhenAll<>?
  A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects. You create a cancellation token by instantiating a CancellationTokenSource object, which manages cancellation tokens retrieved from its CancellationTokenSource.Token property. You then pass the cancellation token to any number of threads, tasks, or operations that should receive notice of cancellation. The token cannot be used to initiate cancellation. When the owning object calls CancellationTokenSource.Cancel, the IsCancellationRequested property on every copy of the cancellation token is set to true. The objects that receive the notification can respond in whatever manner is appropriate.

  ### Declaration
  ```
    public struct CancellationToken
  ```

  ### Example
  ```c#
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        class Program
        {
            static async Task Main()
            {
                var tokenSource2 = new CancellationTokenSource();
                CancellationToken ct = tokenSource2.Token;

                var task = Task.Run(() =>
                {
                    // Were we already canceled?
                    ct.ThrowIfCancellationRequested();

                    bool moreToDo = true;

                    while (moreToDo)
                    {
                        // Poll on this property if you have to do
                        // other cleanup before throwing.
                        if (ct.IsCancellationRequested)
                        {
                            // Clean up here, then...
                            ct.ThrowIfCancellationRequested();
                        }
                    }

                }, tokenSource2.Token); // Pass same token to Task.Run.

                tokenSource2.Cancel();

                // Just continue on this thread, or await with try-catch:
                try
                {
                    await task;
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
                }
                finally
                {
                    tokenSource2.Dispose();
                }

                Console.ReadKey();
            }
        }
    }
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation