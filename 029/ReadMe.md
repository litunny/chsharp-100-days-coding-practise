  # Day 029 - Microsoft C# SortedSet<T>

  ### What is  SortedSet<T> ?
  C#, SortedSet is a collection of objects in sorted order. It is of the generic type collection and defined under System.Collections.Generic namespace. It also provides many mathematical set operations, such as intersection, union, and difference. It is a dynamic collection means the size of the SortedSet is automatically increased when the new elements are added.

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
                // Creating SortedSet
                // Using SortedSet class
                SortedSet<int> mySortedSet = new SortedSet<int>();

                // Add the elements in SortedSet
                // Using Add method
                mySortedSet.Add(101);
                mySortedSet.Add(1001);
                mySortedSet.Add(10001);
                mySortedSet.Add(100001);
                Console.WriteLine("Elements of my_Set1:");

                // Accessing elements of SortedSet
                // Using foreach loop
                foreach (var value in mySortedSet)
                {
                    Console.WriteLine(value);
                }

                // Creating another SortedSet
                // using collection initializer
                // to initialize SortedSet
                SortedSet<int> mySortedSet2 = new SortedSet<int>() {
                                    202,2002,20002,200002};

                // Display elements of my_Set2
                Console.WriteLine("Elements of my_Set2:");
                foreach (var value in mySortedSet2)
                {
                    Console.WriteLine(value);
                }
            }
        }
    }
  ```
  ### References
  *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedset-1?view=net-5.0