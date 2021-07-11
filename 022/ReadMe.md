  # Day 021 - Microsoft C# Self Invoking Method

  ### What is self invoking method?
  
  Although Microsoft C# doesn't have official specification of Self Invoking or Self Executing method. But this can be achieved using Delegate, where function can be invoke or triggered by themselves and perform some certain actions and operations. It's beautiful pattern to use, in this mordern age of development when and where it applies, for performancy and efficiency.

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
                var d = new Func<Dictionary<Int32, String>>(() =>
                {
                    return new Dictionary<Int32, String>
                    {
                        { 0, "Foo" },
                        { 1, "Bar" },
                        { 2, "..." }
                    };
            })();

            Func<String, String> Shorten = s => s.Length > 100 ? s.Substring(0, 100) + "&hellip;" : s;
            }
        }
    }

  ```
  ### References
  *  https://stackoverflow.com/questions/9033/hidden-features-of-c/9099#9099