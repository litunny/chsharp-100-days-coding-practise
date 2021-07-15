  # Day 026 - Microsoft C# Queue<T> Class

  ### What is Queue<T> Class?
  Queue<T> are inserted at one end and removed from the other. Queues and stacks are useful when you need temporary storage for information; that is, when you might want to discard an element after retrieving its value. Use Queue<T> if you need to access the information in the same order that it is stored in the collection. Use Stack<T> if you need to access the information in reverse order.

  ### Example
  ```c#
    using System;
    using System.Collections.Generic;

    namespace ConsoleApp
    {
        class Program
        {
            public static void Main()
            {
                Queue<string> numbers = new Queue<string>();
                numbers.Enqueue("one");
                numbers.Enqueue("two");
                numbers.Enqueue("three");
                numbers.Enqueue("four");
                numbers.Enqueue("five");

                // A queue can be enumerated without disturbing its contents.
                foreach (string number in numbers)
                {
                    Console.WriteLine(number);
                }

                Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
                Console.WriteLine("Peek at next item to dequeue: {0}",
                    numbers.Peek());
                Console.WriteLine("Dequeuing '{0}'", numbers.Dequeue());

                // Create a copy of the queue, using the ToArray method and the
                // constructor that accepts an IEnumerable<T>.
                Queue<string> queueCopy = new Queue<string>(numbers.ToArray());

                Console.WriteLine("\nContents of the first copy:");
                foreach (string number in queueCopy)
                {
                    Console.WriteLine(number);
                }

                // Create an array twice the size of the queue and copy the
                // elements of the queue, starting at the middle of the
                // array.
                string[] array2 = new string[numbers.Count * 2];
                numbers.CopyTo(array2, numbers.Count);

                // Create a second queue, using the constructor that accepts an
                // IEnumerable(Of T).
                Queue<string> queueCopy2 = new Queue<string>(array2);

                Console.WriteLine("\nContents of the second copy, with duplicates and nulls:");
                foreach (string number in queueCopy2)
                {
                    Console.WriteLine(number);
                }

                Console.WriteLine("\nqueueCopy.Contains(\"four\") = {0}",
                    queueCopy.Contains("four"));

                Console.WriteLine("\nqueueCopy.Clear()");
                queueCopy.Clear();
                Console.WriteLine("\nqueueCopy.Count = {0}", queueCopy.Count);
            }
        }
    }

  ```
  ### References
  *  https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2?view=net-5.0