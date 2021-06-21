# Day 001 - Microsoft C# Func<TResult> Delegate

  ### What are Delegates?
  
  A delegate is a type that represents references to methods with a particular parameter list and return type. When you instantiate a delegate, you can associate its instance with   any method with a compatible signature and return type
  
  ### Func<TResult> Delegate
  ```c#
  public delegate TResult Func<out TResult>();
  ```
  
  Encapsulates a method that has no parameters and returns a value of the type specified by the TResult parameter. Delegate are popularly called callbacks, first order functions     and etc in some other programming languages. They simply help to stand as anonymous function without explicitly being defined. We can used them to notify a task as a predicate.   Delegate Func<TResult> can stand out as argument which enables writing of more organized code. 
  
  ### Example - 01
  ```c#
      using System;
      using System.Linq;

      namespace ConsoleApp
      {
          public class Program
          {
              delegate string CustomSelector(string value, int index);
              delegate TResult CustomSelectorTwo <in T, in R, out TResult>(T value, R index);

              static void Main(string[] args)
              {
                  // Note that each lambda expression has no parameters.
                  LazyValue<int> lazyOne = new LazyValue<int>(() => ExpensiveOne());
                  LazyValue<long> lazyTwo = new LazyValue<long>(() => ExpensiveTwo("apple"));

                  Console.WriteLine("LazyValue objects have been created.");

                  // Get the values of the LazyValue objects.
                  Console.WriteLine(lazyOne.value);
                  Console.WriteLine(lazyTwo.value);
  
                  Console.ReadLine();
              }

              private static string SelectorMethod (string value, int index)
              {
                  return value.ToUpper();
              }

              static int ExpensiveOne()
              {
                  Console.WriteLine("\nExpensiveOne() is executing.");
                  return 1;
              }

              static long ExpensiveTwo(string input)
              {
                  Console.WriteLine("\nExpensiveTwo() is executing.");
                  return (long)input.Length;
              }
          }

          class LazyValue<T> where T : struct // This is called constraint, it's been constrained down to a value type
          {
              private Nullable<T> _value; // You can either use Nullable<T> Generic or T? and both still have access to the same method .GetValueOrDefault
              private Func<T> _getValue;
              public T value
              {
                  get
                  {
                      if(_value == null)
                      {
                          _value = _getValue();
                      }
                      return (T)_value;
                  }
              }

              public LazyValue(Func<T> func)
              {
                  _value = null;
                  _getValue = func;
              }
          }
      }

  ```
  ### Let's break it down
  ![GitHub Logo](snaphot-001.png)
 
  In other word ```c# out``` keyword already marked TResult of any type to assignable and modifiable unlike ```c# in ``` and ```c# ref ```. You can read more about the             difference in these parameter modifier here : https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier.
  
  
  ### Func<T, TResult> Delegate
  ```c#
  public delegate TResult Func<in T, out TResult>();
  ```
  
  ### Example - 02 Func<in T, out TResult>()
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace ConsoleApp
    {
        public class Program
        {
            static void Main(string[] args)
            {
                string[] months = { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

                Func<string, string> selector = str => str.ToUpper();

                IEnumerable<string> capitalizedMonths = months.Select(selector);

                foreach (var month in capitalizedMonths)
                {
                    System.Console.WriteLine(month);
                }
            }
        }
    }
  ```
  
  ### Remember Enumerable.Select() has second overload method : 
  
  ```c#
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector);
  ```
  
  The delegate Func<TSource, int, TResult> returns TResult takes in TSource type and can be rewrite in :
  
  ```c#
    Func<string, int, string> selector = delegate (string str, int index) { return SelectorMethod(str, index); };
  
    private static string SelectorMethod (string value, int index)
    {
        //use index for anything
        return value.ToUpper();
    }
  ```
  
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier
