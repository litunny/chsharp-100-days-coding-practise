# Day 012 - Microsoft C# Range

  ### What is C# Range?
  Pattern matching is a technique where you test an expression to determine if it has certain characteristics. C# pattern matching provides more concise syntax for testing expressions and taking action when an expression matches.

  ### Range
  ```c#
  public struct Range : IEquatable<Range>
  ```

  ### Example - Introducing Pattern Matching
  ```c#
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

  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#discard-pattern