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
