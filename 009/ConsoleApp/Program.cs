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
