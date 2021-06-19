using System;

namespace ConsoleApp
{
    public class Program
    {
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
