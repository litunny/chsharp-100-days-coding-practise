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
