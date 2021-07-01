# Day 011 - Microsoft C# Tuple Types 

  ### What is C# Tuple Types?
  The tuples feature provides concise syntax to group multiple data elements in a lightweight data structure, to define a tuple type, you specify types of all its data members and, optionally, the field names. You cannot define methods in a tuple type, but you can use the methods provided by .NET

  ### Example
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                //Without variable
                (double, int) values = (10.0, 10);

                Console.WriteLine($"Item One : {values.Item1} | Item Two : {values.Item2}");

                //With variable
                (string name, int age) person = ("Emmanuel", 90);

                Console.WriteLine($"Name : {person.name} | Age : {person.age}");

                //Using Tuple Fields
                var name = (FirstName: "Emmanuel", LastName: "Osinnowo");

                Console.WriteLine($"First Name : {name.FirstName} | Last Name : {name.LastName}");
            }
        }
    }
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples