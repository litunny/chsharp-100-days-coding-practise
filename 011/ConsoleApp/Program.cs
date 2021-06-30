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