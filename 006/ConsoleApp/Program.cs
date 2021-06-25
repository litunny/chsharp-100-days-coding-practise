using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
namespace ConcurrentBag
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            for (int i = 1; i <= 50; ++i)
            {
                bag.Add(i);
            }
            var task1 = Task.Factory.StartNew(() => {
                while (bag.IsEmpty == false)
                {
                    int item;
                    if (bag.TryTake(out item))
                    {
                        Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                    }
                }
            });
            var task2 = Task.Factory.StartNew(() => {
                while (bag.IsEmpty == false)
                {
                    int item;
                    if (bag.TryTake(out item))
                    {
                        Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                    }
                }
            });
            Task.WaitAll(task1, task2);
            Console.WriteLine("DONE");
        }

    } 
}