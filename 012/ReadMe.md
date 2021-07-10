  # Day 012 - Microsoft C# Pattern Matching

  ### What is C# Pattern Matching?
  Pattern matching is a technique where you test an expression to determine if it has certain characteristics. C# pattern matching provides more concise syntax for testing expressions and taking action when an expression matches.

  ### Example - Introducing Pattern Matching
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                int? age = 99;

                if(age is int realAge)
                {
                    Console.WriteLine($"The age ain't null, Age is {age}");
                } else
                {
                    Console.WriteLine("Invalid age");
                }
                
                var isWeekend = IsWeekWeekend(Day.SATURDAY);

                Console.WriteLine($"IsWeekend : {isWeekend}");

                try
                {
                    var isAdult = IsAdult(0);

                    Console.WriteLine($"isAdult : {isAdult}");

                } catch(Exception e){

                    Console.WriteLine($"Exception : {e.Message}");
                }

                Console.WriteLine("Hello World");
            }

            static bool IsWeekWeekend(Day day) => day switch
            {
                Day.FRIDAY => true,
                Day.SATURDAY => true,
                Day.SUNDAY => true,
                _ => false
            };

            static bool IsAdult(int age) => age switch
            {
                < 1 => throw new ArgumentOutOfRangeException(nameof(age), $"What the hell are you trying to do now?"),
                < 18 => false,
                18 => true,
                > 100 => throw new ArgumentOutOfRangeException(nameof(age), $"You ain't normal!!!"),
                _ => throw new ArgumentOutOfRangeException(nameof(age), $"I give up"),
            };

            static string GetCalendarSeason(DateTime date) => date.Month switch
            {
                >= 3 and < 6 => "spring",
                >= 6 and < 9 => "summer",
                >= 9 and < 12 => "autumn",
                12 or (>= 1 and < 3) => "winter",
                _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
            };
        }

        public enum Day
        {
            MONDAY = 1,
            TUESDAY,
            WEDNESDAY,
            THURSDAY,
            FRIDAY,
            SATURDAY,
            SUNDAY
        }
    }
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#discard-pattern