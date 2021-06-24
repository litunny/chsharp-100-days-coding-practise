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