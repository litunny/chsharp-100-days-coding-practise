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
            //// Note that each lambda expression has no parameters.
            //LazyValue<int> lazyOne = new LazyValue<int>(() => ExpensiveOne());
            //LazyValue<long> lazyTwo = new LazyValue<long>(() => ExpensiveTwo("apple"));

            //Console.WriteLine("LazyValue objects have been created.");

            //// Get the values of the LazyValue objects.
            //Console.WriteLine(lazyOne.value);
            //Console.WriteLine(lazyTwo.value);


            //Func<string, int, string> selector = (str, index) => str.ToUpper();

            Func<string, int, string> selector = delegate (string str, int index) { return SelectorMethod(str, index); };

            string[] months = { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

            var capitalizedWords = months.Select(selector);

            foreach(var month in capitalizedWords)
            {
                Console.WriteLine(month);
            }
            
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

    class LazyValue<T> where T : struct // This is called constraint, it's been constrained down to value type
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
