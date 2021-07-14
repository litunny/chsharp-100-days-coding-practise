  # Day 025 - Microsoft C# KeyValuePair<K, T>

  ### What is Microsoft C# KeyValuePair<K, T> ?
  
  KeyValuePair is the unit of data stored in a Hashtable (or Dictionary). They are not equivalent to each other.

  A key value pair contains a single key and a single value. A dictionary or hashtable contains a mapping of many keys to their associated values.

  KeyValuePair is useful when you want to store two related pieces of information as a single unit, especially when one is related to the other in an identifying way (for instance 1234 => "David Smith"). They are also what you get back when you iterate a dictionary. In .NET 4.0, these are really only meant for use internally in a Dictionary- the Tuple class has been introduced for general purpose use.

  ### Definition
  ```
    public struct KeyValuePair<TKey,TValue>
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
  ```
  ### References
  *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2?view=net-5.0