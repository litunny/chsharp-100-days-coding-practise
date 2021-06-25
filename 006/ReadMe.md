# Day 005 - Microsoft C# ConcurrentBag 

  ### What is C# ConcurrentBag ?
  The ConcurrentBag is one of the thread safe collections that was introduced in .NET 4.0. This collection allows us to store objects in an unordered manner and allows for duplicates. It is useful in a scenario where we do not need to worry about the order in which we would retrieve the objects from the collection. To investigate basic features of the ConcurrentBag and how to add and remove items, please refer to the MSDN documentation.
  
  ### Definition
  ```c#
  ConcurrentBag<T> Class
  ```
  The follow code snippet shows ConcurrentBag in action :
  
  ### Predicate Delegate Example
  ```c#
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    namespace ConcurrentBag
    {
        class Program
        {
            static void Main(string[] args)
            {
                ConcurrentBag<int> bag = new ConcurrentBag<int>();
                for (int i = 1; i <= 50; ++i)
                {
                    bag.Add(i);
                }
                var task1 = Task.Factory.StartNew(() => {
                    while (bag.IsEmpty == false)
                    {
                        int item;
                        if (bag.TryTake(out item))
                        {
                            Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                        }
                    }
                });
                var task2 = Task.Factory.StartNew(() => {
                    while (bag.IsEmpty == false)
                    {
                        int item;
                        if (bag.TryTake(out item))
                        {
                            Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                        }
                    }
                });
                Task.WaitAll(task1, task2);
                Console.WriteLine("DONE");
            }

        } 
    }
  ```

  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-5.0
