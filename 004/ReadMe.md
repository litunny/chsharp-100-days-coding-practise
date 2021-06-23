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
  The number one key factor of to ensuring ensuring object / class serialization is by marking the class with [Serializable] attribute, without this will result to compilation error.

  ### Delegate Chaining Example
  You can use Serialization as an easy means to persist your data, where you can have the current state of your an object of a class ( at that moment) being saved inside a file, and get the same value for this object of a class upon retrieval.

  ### Example - 001
 
  ```c#
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Student student = new Student("John", "Doe");

                FileStream fileStream = new FileStream("student.data", FileMode.Create);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, student);
                fileStream.Close();

                FileStream readFileStream = new FileStream("student.data", FileMode.Open);
                var readStudent = (Student)formatter.Deserialize(readFileStream);
                readFileStream.Close();

                Console.WriteLine(readStudent.GetFullName());

                Console.ReadLine();
            }
        }

        [Serializable]
        class Student
        {
            private string FirstName { get; set; }
            private string LastName { get; set; }
            public Student(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string GetFirstName() => FirstName;
            public string GetLastName() => LastName;
            public string GetFullName() => $"{GetFirstName()} {GetLastName()}";
        }
    }
  ```
  
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.multicastdelegate?view=net-5.0
