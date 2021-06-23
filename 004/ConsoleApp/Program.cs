using System;

namespace ConsoleApp
{
    class Program
    {
        public delegate void Del();
        static void Main (string[] args)
        {
            

            Del a = delegate { Console.WriteLine("This is A - Delegate"); };
            Del b = delegate { Console.WriteLine("This is B - Delegate"); };
            Del c = a + b;
            Del d = c - a;

            Console.WriteLine("Delegate result for c");
            c();

            Console.WriteLine("Delegate result for d");
            d();


            MulticastDelegate.Initialize()
               .MakeAction(() => {
                   Console.WriteLine("Make Action");
               })
               .Then(() => {
                   Console.WriteLine("Then");
               })
               .Then(() =>
               {
                   Console.WriteLine("Then 2");
               })
               .AndThen(() => {
                   Console.WriteLine("And Then");
               })
               .AndFinally(() =>
               {
                   Console.WriteLine("And Finally");
               })
               .Then(() =>
               {
                   Console.WriteLine("Another Then");
               })
               .Build();

            Console.WriteLine("Hello World");
        }
    }

    public class MulticastDelegate
    {
        public delegate void Del();
        private static MulticastDelegate instance = new MulticastDelegate();
        private Del dels;
        private MulticastDelegate() { }
        public static MulticastDelegate Initialize()
        {
            return instance;
        }
        public MulticastDelegate MakeAction(Del del)
        {
            dels += del;
            return this;
        }
        public MulticastDelegate AndThen(Del del)
        {
            dels += del;
            return this;
        }
        public MulticastDelegate AndFinally (Del del)
        {
            dels += del;
            return this;
        }
        public MulticastDelegate Then(Del del)
        {
            dels += del;
            return this;
        }
        public void Build()
        {
            dels();
        }
    }
}