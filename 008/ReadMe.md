# Day 007 - Microsoft C# Lazy<T> 

  ### What is C# Lazy<T> ?
  Lazy initialization is a technique that defers the creation of an object until the first time it is needed. In other words, initialization of the object happens only on demand.
  
  Lazy<T> can improve the application’s performance by avoiding unnecessary computation and memory consumption.

  ### Lazy<T> Example
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Lazy<IEnumerable<string>> listOfNames = new Lazy<IEnumerable<string>>(() => new List<string> { "Michael", "Miracle", "Mika", "Mendes"}) ;

                foreach(var name in listOfNames.Value)
                {
                    Console.WriteLine($"The name: {name}");
                }

                Console.WriteLine("Hello World");
            }
        }
    }

  ```

  ### References
  * https://www.infoworld.com/article/3227207/how-to-perform-lazy-initialization-in-c.html
  * https://docs.microsoft.com/en-us/dotnet/api/system.lazy-1?view=net-5.0
