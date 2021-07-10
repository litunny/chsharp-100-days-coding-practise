  # Day 019 - Microsoft C# WeakReference

  ### What is C# WeakReference ?
  A weak reference is a reference, that allows the GC to collect the object while still allowing to access the object. A weak reference is valid only during the indeterminate amount of time until the object is collected when no strong references exist. When you use a weak reference, the application can still obtain a strong reference to the object, which prevents it from being collected. So weak references can be useful for holding on to large objects that are expensive to initialize, but should be available for garbage collection if they are not actively in use.
 
  ### Declaration
  ```c#
    public class WeakReference : System.Runtime.Serialization.ISerializable
  ```
  ### Example
  ```c#
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
  ```
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.weakreference?view=net-5.0