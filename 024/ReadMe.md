  # Day 024 - Microsoft C# List.ForEach(Action<T>)

  ### What is List.ForEach(Action<T>)?
  
  The ForEach method of the List<T> (not IList<T>) executes an operation for every object which is stored in the list. Normally it contains code to either read or modify every object which is in the list or to do something with list itself for every object.

  ### Definition
  ```
    public void ForEach (Action<T> action);
  ```
  ### Example
  ```c#
    using System;
    using System.Collections.Generic;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                List<string> names = new List<string>();
                names.Add("Bruce");
                names.Add("Alfred");
                names.Add("Tim");
                names.Add("Richard");

                // Display the contents of the list using the Print method.
                names.ForEach(Print);

                // The following demonstrates the anonymous method feature of C#
                // to display the contents of the list to the console.
                names.ForEach(delegate (String name)
                {
                    Console.WriteLine(name);
                });

                void Print(string s)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }

  ```
  ### References
  *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.foreach?view=net-5.0