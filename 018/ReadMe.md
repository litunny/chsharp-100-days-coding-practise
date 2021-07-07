# Day 018 - Microsoft C# 9.0 Record Types  

  ### What is C# 9.0 Record Types ?
  C# 9.0 introduces record types. You use the record keyword to define a reference type that provides built-in functionality for encapsulating data. You can create record types with immutable properties by using positional parameters or standard property syntax:
 
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
                List<Person> persons = new();

                Person person = new("Kelvin", "Hart");

                Console.WriteLine(person.FirstName);

                Person person2 = person with { FirstName = "Michael" };

                Console.WriteLine(person2.FirstName);
            }
        }

        public record Person(string FirstName, string LastName);
    }
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types