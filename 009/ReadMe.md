# Day 007 - Microsoft C# Tuple<T> 

  ### What is C# Tupe<T>?
  A tuple is a data structure that has a specific number and sequence of elements. The .NET Framework directly supports tuples with one to seven elements. In addition, you can create tuples of eight or more elements by nesting tuple objects in the Rest property of a

  ### Tuple<T, T, T> Example
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                var values = Tuple.Create("Fance", "Nigeria", 1890, "Maxwell");

                Console.WriteLine(values.Item1);

                (var firstName, var lastName, var age) = new Tuple<string, string, int>("John", "Mendes", 99);

                Console.WriteLine($"First Name : {firstName} | Last Name : {lastName} | Age : {age}");

                Console.ReadLine();
            }
        }
    }

  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.tuple?view=net-5.0