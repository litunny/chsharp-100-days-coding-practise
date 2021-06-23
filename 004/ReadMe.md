# Day 004 - Microsoft C# Multicast Delegate

  ### What Multicast Delegate?
  Multicast Delegate is a delegate that can have more than one element in its invocation. This delegate specially holds reference of more than one function. When this delegate is invoked, then all the functions which are referenced by the delegate are going to be invoked. 

  ### Multicast Delegate Example
  ```c#
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
            }
        }
    }
  ```
  Despite that delegate serves as a pointer, type or blueprint to methods, using multicast delegate can be subject to having arithmetic operator acting on them. Also the following example shows how you can chain delegate together for order of executions.

  ### Delegate Chaining Example
  ```c#
    using System;
    namespace ConsoleApp
    {
        class Program
        {
            public delegate void Del();
            static void Main (string[] args)
            {
              
                MulticastDelegate.Initialize()
                  .MakeAction(() => {
                      Console.WriteLine("Make Action");
                  })
                  .Then(() => {
                      Console.WriteLine("Then");
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
  ```
    
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.multicastdelegate?view=net-5.0
