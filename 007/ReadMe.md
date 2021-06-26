# Day 007 - Microsoft C# BlockingCollection<T> 

  ### What is C# BlockingCollection ?
  A generic collection that supports bounding and blocking. Bounding means you can set the maximum capacity of the collection. Bounding is important in certain scenarios because it enables you to control the maximum size of the collection in memory, and it prevents the producing threads from moving too far ahead of the consuming threads.
  
  The following example shows a simple BlockingCollection with a bounded capacity of 100. A producer task adds items to the collection as long as some external condition is true, and then calls CompleteAdding. The consumer task takes items until the IsCompleted property is true.
  
  ### BlockingCollection<T> Example
  ```c#
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        class Program
        {
            private static bool moreItemsToAdd = true;
            static void Main(string[] args)
            {
                // A bounded collection. It can hold no more
                // than 100 items at once.
                BlockingCollection<Data> dataItems = new BlockingCollection<Data>(100);
                
                // A simple blocking consumer with no cancellation.
                Task.Run(() =>
                    {
                        while (!dataItems.IsCompleted)
                        {

                            Data data = null;
                            // Blocks if dataItems.Count == 0.
                            // IOE means that Take() was called on a completed collection.
                            // Some other thread can call CompleteAdding after we pass the
                            // IsCompleted check but before we call Take.
                            // In this example, we can simply catch the exception since the
                            // loop will break on the next iteration.
                            try
                            {
                                data = dataItems.Take();
                            }
                            catch (InvalidOperationException) { }

                            if (data != null)
                            {
                                Process(data);
                            }
                        }
                        Console.WriteLine("\r\nNo more items to take.");
                    });

                    // A simple blocking producer with no cancellation.
                    Task.Run(() =>
                    {
                        while (moreItemsToAdd)
                        {
                            Data data = GetData();
                            // Blocks if numbers.Count == dataItems.BoundedCapacity
                            dataItems.Add(data);
                        }
                        // Let consumer know we are done.
                        dataItems.CompleteAdding();
                    });
                }

            public static Data GetData() => new Data("Sample");

            public static void Process (Data data)
            {
                Console.WriteLine($"Processing....{data.Property}");
            }
        }

        public class Data
        {
            public Data(string property)
            {
                Property = property;
            }
            public string Property { get; set; }
        }
    }

  ```

  ### References
  * https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/blockingcollection-overview
