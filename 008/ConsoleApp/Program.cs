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