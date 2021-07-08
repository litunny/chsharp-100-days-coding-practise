using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WeakReference reference = new WeakReference(new object(), false);

            GC.Collect();

            object target = reference.Target;
            if (target != null)
                DoSomething(target);
        }

        private static void DoSomething(object obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }
}
