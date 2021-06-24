# Day 005 - Microsoft C# Predicate Delegate

  ### What Predicate Delegate?
  Predicate is the delegate like Func and Action delegates. It represents a method containing a set of criteria and checks whether the passed parameter meets those criteria. A predicate delegate methods must take one input parameter and return a boolean - true or false.

  ### Predicate Delegate Example
  ```c#
    using System;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Predicate<string> predicate = delegate (string value) { return value.Contains("Emmanuel"); };

                Predicate<string> predicate1 = (string value) => value.Contains("Joshua");

                Console.WriteLine(predicate("Osinnowo Itunu"));

                var result = Array.Find<string>(new string[] { "Joshua", "Temi", "Emmanuel4" }, s => s.Contains("Emmanuel"));

                Console.WriteLine(result);

                Console.ReadLine();
            }
        }
    }
  ```

  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-5.0
