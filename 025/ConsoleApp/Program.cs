using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList = new List<KeyValuePair<string, int>>();
            // adding elements
            myList.Add(new KeyValuePair<string, int>("Laptop", 1));
            myList.Add(new KeyValuePair<string, int>("Desktop System", 2));
            myList.Add(new KeyValuePair<string, int>("Tablet", 3));
            myList.Add(new KeyValuePair<string, int>("Mobile", 4));
            myList.Add(new KeyValuePair<string, int>("E-Book Reader", 5));
            myList.Add(new KeyValuePair<string, int>("LED", 6));

            foreach (var val in myList)
            {
                Console.WriteLine(val);
            }
        }
    }
}