using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
                          
            
            int[] subArray1 = someArray[0..2];

            foreach (var num in subArray1)
            {
                Console.WriteLine($"SubArray1 Num : {num}");
            }

            int[] subArray2 = someArray[1..^0];

            foreach (var num in subArray2)
            {
                Console.WriteLine($"SubArray2 Num : {num}");
            }
        }
    }
}
