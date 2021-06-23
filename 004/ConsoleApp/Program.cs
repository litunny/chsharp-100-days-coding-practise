using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main (string[] args)
        {
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