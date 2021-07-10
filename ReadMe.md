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
  ![GitHub Logo](001/snaphot-001.png)
 
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



# Day 002 - Microsoft C# Action<T> Delegate

  ### Action<T> Delegate
  ```c#
  public delegate void Action<in T>(T obj);
  ```
  Similar to the Func<TResult> Delegate, they both almost performed the same function except that the Action<T> doesn't allow for the return of a value, while Func<TResult> allows to return a value (reference). Both can take zero, one or more input parameters and perform the specific tasks in the body of the method.
  
  ### Example - 01 
  ```c#
    using System;
    namespace ConsoleApp
    {
        public class Program
        {
            static void Main(string[] args)
            {
                Action action = delegate () { Console.WriteLine("Method Fired!!!"); };

                Action<string> action1 = delegate (string value) { Console.WriteLine(value); };

                Action<string, string> action2 = (string value1, string value2) => { Console.WriteLine($"{value1} and {value2}"); };

                action();
                action1("Value One");
                action2("Value One", "Value Two");
            }
        }
    }
  ```
  
  One of the most obvious and perfect example of Action<T> in action is List.ForEach(Action<T> action), where it takes an Action<T> as an argument to perform iterable operation on each item of the List.

  ```c#
        List<string> names = new List<string>();
        names.add("Willy");
        names.add("Cornor");
        names.add("Colson");

        names.ForEach(Print);

        names.ForEach(delegate(string name) {
            Console.WriteLine(s);
        });

        void Print(string s) {
            Console.WriteLine(s);
        }
  ```
  
  ### More Examples 
  
  Below is another example, more like a pragmatic approach where you can use Action<T> and Action<T, T, T> in many more. The below implementation is just an abstract or pseudocode for a Fund Transfer Service:
  
  ### Example - 02 Action<in T, in T, in T>()
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        public class Program
        {
            static async Task Main(string[] args)
            {
                //Initiate Transfer Service
                TransferService<UserAccount> transferService = new TransferService<UserAccount>(() => { return GetMyUserAccount(); });

                //Send money to beneficiary
                await transferService.SendMoneyAsync(GetBeneficiaryUserAccount(), callback: async (account, response, status) => {

                    var result = status switch
                    {
                        TransferStatus.APPROVED => "Approved",
                        TransferStatus.PENDING => "Pending",
                        TransferStatus.FAILED => "Failed",
                        _ => "Declined",
                    };

                    new List<string>() { }.ForEach(x => { });

                    //Giving response base on the response status and message
                    Console.WriteLine(response);

                    if (result.ToLower() == "pending")
                    {
                        //TODO: Calling an external service for requery or notification
                        await Task.Delay(2000);
                    }

                    //TODO: Notifying customer based on the transaction
                    Console.WriteLine("Completed!!!");
                });

                Console.ReadLine();
            }

            public static UserAccount GetBeneficiaryUserAccount()
            {
                return new UserAccount("Michel Clarke", "Sterling Bank", "XXXXXXXXXX", 120.00);
            }

            public static UserAccount GetMyUserAccount ()
            {
                return new UserAccount("John Doe", "Sterling Bank", "XXXXXXXXXX", 90.00);
            }

            public static void Transfer()
            {
                Console.WriteLine("Transfering...");
            }
        }

        class TransferService <T> where T : UserAccount
        {
            private T _Sender;

            private Func<T> _GetSender;
            private T Sender
            {
                get
                {
                    if(_Sender == null)
                    {
                    _Sender = _GetSender();
                    }
                    return _Sender;
                }
            }
            public TransferService (Func<T> GetSender)
            {
                _Sender = null;
                _GetSender = GetSender;
            }

            public async Task SendMoneyAsync(UserAccount beneficiaryAccount, Action<UserAccount, string, TransferStatus> callback)
            {
                Console.WriteLine("Processing...");
                Console.WriteLine($"Sending {Sender.GetAmount()} to {Sender.GetBank()} - {Sender.GetAccountNumber()}");
                await Task.Delay(5000);
                Console.WriteLine("Sent!!!");
                callback(beneficiaryAccount, $"Your transaction of ${beneficiaryAccount.GetAmount()} to {beneficiaryAccount.GetAccountName()} is still processing", TransferStatus.PENDING);
            }
        }

        public class UserAccount : IUserAccount
        {
            private  string AccountName { get; set; }
            private string Bank { get; set; }
            private string AccountNumber { get; set; }
            private double Amount { get; set; }

            private UserAccount () { }
            public UserAccount(string accountName, string bank, string accountNumber, double amount)
            {
                AccountName = accountName;
                Bank = bank;
                AccountNumber = accountNumber;
                Amount = amount;
            }

            public string GetBank() => Bank;
            public string GetAccountNumber() => AccountNumber;
            public double GetAmount() => Amount;
            public string GetAccountName() => AccountName;
        }

        public interface IUserAccount
        {
            public string GetBank();
            public string GetAccountNumber();
            public double GetAmount();
            public string GetAccountName();
        }

        public enum TransferStatus
        {
            APPROVED = 1,
            PENDING,
            FAILED,
            DECLINED
        }
    }
  ```
  


  # Day 003 - Microsoft C# Serialization

  ### What is Serialization?
  
  Serialization is the process of converting an object into a stream of bytes to store the object or transmit it to memory, a database, or a file. Its main purpose is to save the state of an object in order to be able to recreate it when needed. The reverse process is called deserialization.
  
  ### Using ISerializable Interface
  ```c#
  public interface ISerializable
  ```
  This interface allows for custom serialization mechanism, where each property of a class inheriting can have their definition of storing their respective properties. One of the key remark to take note is that, any class inheriting from this ISerializable must conform to the following constructor:

  ```c#
    public ClassName (SerializationInfo info, StreamingContext context)
  ```
  The number one key factor of to ensuring ensuring object / class serialization is by marking the class with [Serializable] attribute, without this will result to compilation error.

  ### Benefits
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
    


  # Day 005 - Microsoft C# Predicate Delegate

  ### What is Predicate Delegate?
  Predicate is the delegate like Func and Action delegates. It represents a method containing a set of criteria and checks whether the passed parameter meets those criteria. A predicate delegate methods must take one input parameter and return a boolean - true or false.

  ### Definition
  ```c#
  public delegate bool Predicate<in T>(T obj)
  ```
  The follow code snippet shows Predicate delegate usage in action :
  
  ### Predicate Delegate Example
  ```c#
    using System;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Predicate<string> predicate = delegate (string value) { return value.Contains("Emmanuel"); };

                Predicate<string> predicate1 = (string value) => value.Contains("Joshua");

                Console.WriteLine(predicate("Osinnowo Itunu"));

                var result = Array.Find<string>(new string[] { "Joshua", "Temi", "Emmanuel4" }, s => s.Contains("Emmanuel"));

                Console.WriteLine(result);

                Console.ReadLine();
            }
        }
    }
  ```



  # Day 006 - Microsoft C# ConcurrentBag 

  ### What is C# ConcurrentBag ?
  The ConcurrentBag is one of the thread safe collections that was introduced in .NET 4.0. This collection allows us to store objects in an unordered manner and allows for duplicates. It is useful in a scenario where we do not need to worry about the order in which we would retrieve the objects from the collection. To investigate basic features of the ConcurrentBag and how to add and remove items, please refer to the MSDN documentation.
  
  ### Definition
  ```c#
  ConcurrentBag<T> Class
  ```
  The follow code snippet shows ConcurrentBag in action :
  
  ### Predicate Delegate Example
  ```c#
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    namespace ConcurrentBag
    {
        class Program
        {
            static void Main(string[] args)
            {
                ConcurrentBag<int> bag = new ConcurrentBag<int>();
                for (int i = 1; i <= 50; ++i)
                {
                    bag.Add(i);
                }
                var task1 = Task.Factory.StartNew(() => {
                    while (bag.IsEmpty == false)
                    {
                        int item;
                        if (bag.TryTake(out item))
                        {
                            Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                        }
                    }
                });
                var task2 = Task.Factory.StartNew(() => {
                    while (bag.IsEmpty == false)
                    {
                        int item;
                        if (bag.TryTake(out item))
                        {
                            Console.WriteLine($"{item} was picked by {Task.CurrentId}");
                        }
                    }
                });
                Task.WaitAll(task1, task2);
                Console.WriteLine("DONE");
            }

        } 
    }
  ```


  # Day 007 - Microsoft C# BlockingCollection<T> 

  ### What is C# BlockingCollection ?
  A generic collection that supports bounding and blocking. Bounding means you can set the maximum capacity of the collection. Bounding is important in certain scenarios because it enables you to control the maximum size of the collection in memory, and it prevents the producing threads from moving too far ahead of the consuming threads.
  
  The following example shows a simple BlockingCollection with a bounded capacity of 100. A producer task adds items to the collection as long as some external condition is true, and then calls CompleteAdding. The consumer task takes items until the IsCompleted property is true.
  
  ### BlockingCollection<T> Example
  ```c#
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        class Program
        {
            private static bool moreItemsToAdd = true;
            static void Main(string[] args)
            {
                // A bounded collection. It can hold no more
                // than 100 items at once.
                BlockingCollection<Data> dataItems = new BlockingCollection<Data>(100);
                
                // A simple blocking consumer with no cancellation.
                Task.Run(() =>
                    {
                        while (!dataItems.IsCompleted)
                        {

                            Data data = null;
                            // Blocks if dataItems.Count == 0.
                            // IOE means that Take() was called on a completed collection.
                            // Some other thread can call CompleteAdding after we pass the
                            // IsCompleted check but before we call Take.
                            // In this example, we can simply catch the exception since the
                            // loop will break on the next iteration.
                            try
                            {
                                data = dataItems.Take();
                            }
                            catch (InvalidOperationException) { }

                            if (data != null)
                            {
                                Process(data);
                            }
                        }
                        Console.WriteLine("\r\nNo more items to take.");
                    });

                    // A simple blocking producer with no cancellation.
                    Task.Run(() =>
                    {
                        while (moreItemsToAdd)
                        {
                            Data data = GetData();
                            // Blocks if numbers.Count == dataItems.BoundedCapacity
                            dataItems.Add(data);
                        }
                        // Let consumer know we are done.
                        dataItems.CompleteAdding();
                    });
                }

            public static Data GetData() => new Data("Sample");

            public static void Process (Data data)
            {
                Console.WriteLine($"Processing....{data.Property}");
            }
        }

        public class Data
        {
            public Data(string property)
            {
                Property = property;
            }
            public string Property { get; set; }
        }
    }
  ```



  # Day 008 - Microsoft C# Lazy<T> 
  
  ### What is C# Lazy<T> ?
  Lazy initialization is a technique that defers the creation of an object until the first time it is needed. In other words, initialization of the object happens only on demand.
  
  Lazy<T> can improve the application’s performance by avoiding unnecessary computation and memory consumption.

  ### Lazy<T> Example

  ```c#
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                Lazy<IEnumerable<string>> listOfNames = new Lazy<IEnumerable<string>>(() => new List<string> { "Michael", "Miracle", "Mika", "Mendes"}) ;

                foreach(var name in listOfNames.Value)
                {
                    Console.WriteLine($"The name: {name}");
                }

                Console.WriteLine("Hello World");
            }
        }
    }

  ```



  # Day 009 - Microsoft C# Tuple 

  ### What is C# Tuple?
  A tuple is a data structure that has a specific number and sequence of elements. The .NET Framework directly supports tuples with one to seven elements. In addition, you can create tuples of eight or more elements by nesting tuple objects in the Rest property of a Tuple<T1,T2,T3,T4,T5,T6,T7,TRest> object.

  ### Tuple<T, T, T> Example
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                var values = Tuple.Create("Fance", "Nigeria", 1890, "Maxwell");

                Console.WriteLine(values.Item1);

                (var firstName, var lastName, var age) = new Tuple<string, string, int>("John", "Mendes", 99);

                Console.WriteLine($"First Name : {firstName} | Last Name : {lastName} | Age : {age}");

                Console.ReadLine();
            }
        }
    }

  ```



  # Day 010 - Microsoft C# IObservable 

  ### What is C# IObservable?
  IObservable has a Subscribe method that must be implemented. It represents the registration of observers and returns an IDisposable object. As it returns an IDisposable it will be easier for us to release an observer from the subject properly. 

  ### Example
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                var provider = new StockTrader();
                var i1 = new Investor();
                i1.Subscribe(provider);
                var i2 = new Investor();
                i2.Subscribe(provider);

                provider.Trade(new Stock());
                provider.Trade(new Stock());
                provider.Trade(null);
                provider.End();
            }
        }

        public class Stock
        {
            private string Symbol { get; set; }
            private decimal Price { get; set; }
        }

        public class Investor : IObserver<Stock>
        {
            public IDisposable unsubscriber;
            public virtual void Subscribe(IObservable<Stock> provider)
            {
                if (provider != null)
                {
                    unsubscriber = provider.Subscribe(this);
                }
            }
            public virtual void OnCompleted()
            {
                unsubscriber.Dispose();
            }
            public virtual void OnError(Exception e)
            {
            }
            public virtual void OnNext(Stock stock)
            {
            }
        }

        public class StockTrader : IObservable<Stock>
        {
            public StockTrader()
            {
                observers = new List<IObserver<Stock>>();
            }
            private IList<IObserver<Stock>> observers;
            public IDisposable Subscribe(IObserver<Stock> observer)
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
                return new Unsubscriber(observers, observer);
            }
            public class Unsubscriber : IDisposable
            {
                private IList<IObserver<Stock>> _observers;
                private IObserver<Stock> _observer;

                public Unsubscriber(IList<IObserver<Stock>> observers, IObserver<Stock> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }

                public void Dispose()
                {
                    Dispose(true);
                }
                private bool _disposed = false;
                protected virtual void Dispose(bool disposing)
                {
                    if (_disposed)
                    {
                        return;
                    }
                    if (disposing)
                    {
                        if (_observer != null && _observers.Contains(_observer))
                        {
                            _observers.Remove(_observer);
                        }
                    }
                    _disposed = true;
                }
            }
            public void Trade(Stock stock)
            {
                foreach (var observer in observers)
                {
                    if (stock == null)
                    {
                        observer.OnError(new ArgumentNullException());
                    }
                    observer.OnNext(stock);
                }
            }
            public void End()
            {
                foreach (var observer in observers.ToArray())
                {
                    observer.OnCompleted();
                }
                observers.Clear();
            }
        }
    }

  ```



  # Day 011 - Microsoft C# Tuple Types 

  ### What is C# Tuple Types?
  The tuples feature provides concise syntax to group multiple data elements in a lightweight data structure, to define a tuple type, you specify types of all its data members and, optionally, the field names. You cannot define methods in a tuple type, but you can use the methods provided by .NET

  ### Example
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                //Without variable
                (double, int) values = (10.0, 10);

                Console.WriteLine($"Item One : {values.Item1} | Item Two : {values.Item2}");

                //With variable
                (string name, int age) person = ("Emmanuel", 90);

                Console.WriteLine($"Name : {person.name} | Age : {person.age}");

                //Using Tuple Fields
                var name = (FirstName: "Emmanuel", LastName: "Osinnowo");

                Console.WriteLine($"First Name : {name.FirstName} | Last Name : {name.LastName}");
            }
        }
    }
  ```



  # Day 012 - Microsoft C# Pattern Matching

  ### What is C# Pattern Matching?
  Pattern matching is a technique where you test an expression to determine if it has certain characteristics. C# pattern matching provides more concise syntax for testing expressions and taking action when an expression matches.

  ### Example - Introducing Pattern Matching
  ```c#
    using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                int? age = 99;

                if(age is int realAge)
                {
                    Console.WriteLine($"The age ain't null, Age is {age}");
                } else
                {
                    Console.WriteLine("Invalid age");
                }
                
                var isWeekend = IsWeekWeekend(Day.SATURDAY);

                Console.WriteLine($"IsWeekend : {isWeekend}");

                try
                {
                    var isAdult = IsAdult(0);

                    Console.WriteLine($"isAdult : {isAdult}");

                } catch(Exception e){

                    Console.WriteLine($"Exception : {e.Message}");
                }

                Console.WriteLine("Hello World");
            }

            static bool IsWeekWeekend(Day day) => day switch
            {
                Day.FRIDAY => true,
                Day.SATURDAY => true,
                Day.SUNDAY => true,
                _ => false
            };

            static bool IsAdult(int age) => age switch
            {
                < 1 => throw new ArgumentOutOfRangeException(nameof(age), $"What the hell are you trying to do now?"),
                < 18 => false,
                18 => true,
                > 100 => throw new ArgumentOutOfRangeException(nameof(age), $"You ain't normal!!!"),
                _ => throw new ArgumentOutOfRangeException(nameof(age), $"I give up"),
            };

            static string GetCalendarSeason(DateTime date) => date.Month switch
            {
                >= 3 and < 6 => "spring",
                >= 6 and < 9 => "summer",
                >= 9 and < 12 => "autumn",
                12 or (>= 1 and < 3) => "winter",
                _ => throw new ArgumentOutOfRangeException(nameof(date), $"Date with unexpected month: {date.Month}."),
            };
        }

        public enum Day
        {
            MONDAY = 1,
            TUESDAY,
            WEDNESDAY,
            THURSDAY,
            FRIDAY,
            SATURDAY,
            SUNDAY
        }
    }
  ```



  # Day 013 - Microsoft C# Range

  ### What is C# Range?
  Pattern matching is a technique where you test an expression to determine if it has certain characteristics. C# pattern matching provides more concise syntax for testing expressions and taking action when an expression matches.

  ### Range
  ```c#
  public struct Range : IEquatable<Range>
  ```

  ### Example - Introducing Pattern Matching
  ```c#
   using System;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
                            
                
                int[] subArray1 = someArray[0..2];

                foreach (var num in subArray1)
                {
                    Console.WriteLine($"SubArray1 Num : {num}");
                }

                int[] subArray2 = someArray[1..^0];

                foreach (var num in subArray2)
                {
                    Console.WriteLine($"SubArray2 Num : {num}");
                }
            }
        }
    }

  ```




# Day 014 - Microsoft C# Task.WhenAny()

  ### What is C# Task.WhenAny()?
  Creates a task that will complete when any of the supplied tasks have completed. The returned task will complete when any of the supplied tasks has completed. The returned task will always end in the RanToCompletion state with its Result set to the first task to complete. This is true even if the first task to complete ended in the Canceled or Faulted state.

  ### Range
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        class Program
        {
            static async Task Main(string[] args)
            {
                Coffee cup = PourCoffee();
                Console.WriteLine("coffee is ready");

                var eggsTask = FryEggsAsync(2);
                var baconTask = FryBaconAsync(3);
                var toastTask = MakeToastWithButterAndJamAsync(2);

                var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
                while (breakfastTasks.Count > 0)
                {
                    Task finishedTask = await Task.WhenAny(breakfastTasks);

                    if (finishedTask == eggsTask)
                    {
                        Console.WriteLine("eggs are ready");
                    }
                    else if (finishedTask == baconTask)
                    {
                        Console.WriteLine("bacon is ready");
                    }
                    else if (finishedTask == toastTask)
                    {
                        Console.WriteLine("toast is ready");
                    }
                    breakfastTasks.Remove(finishedTask);
                }

                Juice oj = PourOJ();
                Console.WriteLine("oj is ready");
                Console.WriteLine("Breakfast is ready!");
            }

            static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
            {
                var toast = await ToastBreadAsync(number);
                ApplyButter(toast);
                ApplyJam(toast);

                return toast;
            }

            private static Juice PourOJ()
            {
                Console.WriteLine("Pouring orange juice");
                return new Juice();
            }

            private static void ApplyJam(Toast toast) =>
                Console.WriteLine("Putting jam on the toast");

            private static void ApplyButter(Toast toast) =>
                Console.WriteLine("Putting butter on the toast");

            private static async Task<Toast> ToastBreadAsync(int slices)
            {
                for (int slice = 0; slice < slices; slice++)
                {
                    Console.WriteLine("Putting a slice of bread in the toaster");
                }
                Console.WriteLine("Start toasting...");
                await Task.Delay(3000);
                Console.WriteLine("Remove toast from toaster");

                return new Toast();
            }

            private static async Task<Bacon> FryBaconAsync(int slices)
            {
                Console.WriteLine($"putting {slices} slices of bacon in the pan");
                Console.WriteLine("cooking first side of bacon...");
                await Task.Delay(3000);
                for (int slice = 0; slice < slices; slice++)
                {
                    Console.WriteLine("flipping a slice of bacon");
                }
                Console.WriteLine("cooking the second side of bacon...");
                await Task.Delay(3000);
                Console.WriteLine("Put bacon on plate");

                return new Bacon();
            }

            private static async Task<Egg> FryEggsAsync(int howMany)
            {
                Console.WriteLine("Warming the egg pan...");
                await Task.Delay(3000);
                Console.WriteLine($"cracking {howMany} eggs");
                Console.WriteLine("cooking the eggs ...");
                await Task.Delay(3000);
                Console.WriteLine("Put eggs on plate");

                return new Egg();
            }

            private static Coffee PourCoffee()
            {
                Console.WriteLine("Pouring coffee");
                return new Coffee();
            }
        }

        class Coffee { }
        class Toast { }
        class Juice { }
        class Bacon { }
        class Egg { }
    }
  ```




  # Day 015 - Microsoft C# Task.WhenAll(IEnumerable<Task>)

  ### What is C# Task.WhenAll(IEnumerable<Task>)?
  WhenAll(IEnumerable<Task>) Creates a task that will complete when all of the Task objects in an enumerable collection have completed. WhenAll(Task[]) Creates a task that will complete when all of the Task objects in an array have completed.

  ### Range
  ```c#
    using System;
    using System.Collections.Generic;
    using System.Net.NetworkInformation;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        public class Program
        {
            public static async Task Main()
            {
                int failed = 0;
                var tasks = new List<Task>();
                String[] urls = { "www.adatum.com", "www.cohovineyard.com",
                            "www.cohowinery.com", "www.northwindtraders.com",
                            "www.contoso.com" };

                foreach (var value in urls)
                {
                    var url = value;
                    tasks.Add(Task.Run(() => {
                        var png = new Ping();
                        try
                        {
                            var reply = png.Send(url);
                            if (!(reply.Status == IPStatus.Success))
                            {
                                Interlocked.Increment(ref failed);
                                throw new TimeoutException("Unable to reach " + url + ".");
                            }
                        }
                        catch (PingException)
                        {
                            Interlocked.Increment(ref failed);
                            throw;
                        }
                    }));
                }
                Task t = Task.WhenAll(tasks.ToArray());
                try
                {
                    await t;
                }
                catch { }

                if (t.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("All ping attempts succeeded.");
                else if (t.Status == TaskStatus.Faulted)
                    Console.WriteLine("{0} ping attempts failed", failed);
            }
        }
    }
  ```
 




  # Day 016 - Microsoft C# Cancellation Token

  ### What is C# Cancellation Token?
  A CancellationToken enables cooperative cancellation between threads, thread pool work items, or Task objects. You create a cancellation token by instantiating a CancellationTokenSource object, which manages cancellation tokens retrieved from its CancellationTokenSource.Token property. You then pass the cancellation token to any number of threads, tasks, or operations that should receive notice of cancellation. The token cannot be used to initiate cancellation. When the owning object calls CancellationTokenSource.Cancel, the IsCancellationRequested property on every copy of the cancellation token is set to true. The objects that receive the notification can respond in whatever manner is appropriate.

  ### Declaration
  ```
    public struct CancellationToken
  ```

  ### Example
  ```c#
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    namespace ConsoleApp
    {
        class Program
        {
            static async Task Main()
            {
                var tokenSource2 = new CancellationTokenSource();
                CancellationToken ct = tokenSource2.Token;

                var task = Task.Run(() =>
                {
                    // Were we already canceled?
                    ct.ThrowIfCancellationRequested();

                    bool moreToDo = true;

                    while (moreToDo)
                    {
                        // Poll on this property if you have to do
                        // other cleanup before throwing.
                        if (ct.IsCancellationRequested)
                        {
                            // Clean up here, then...
                            ct.ThrowIfCancellationRequested();
                        }
                    }

                }, tokenSource2.Token); // Pass same token to Task.Run.

                tokenSource2.Cancel();

                // Just continue on this thread, or await with try-catch:
                try
                {
                    await task;
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
                }
                finally
                {
                    tokenSource2.Dispose();
                }

                Console.ReadKey();
            }
        }
    }
  ```




  # Day 017 - Microsoft C# File.Create() 

  ### What is C# File.Create() ?
  The Create() method of the File class is used to create files in C#. The File. Create() method takes a fully specified path as a parameter and creates a file at the specified location; if any such file already exists at the given location, it is overwritten.

  ### Declaration
  ```
    public static System.IO.FileStream Create (string path);
  ```

  ### Example
  ```c#
    using System;
    using System.IO;
    using System.Text;

    namespace ConsoleApp {
        class Program
        {
            public static void Main()
            {
                string path = @"c:\temp\MyTest.txt";

                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }

                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
  ```




  # Day 018 - Microsoft C# 9.0 Record Types  

  ### What is C# 9.0 Record Types ?
  C# 9.0 introduces record types. You use the record keyword to define a reference type that provides built-in functionality for encapsulating data. You can create record types with immutable properties by using positional parameters or standard property syntax:
 
  ### Example
  ```c#
    using System;
    using System.Collections.Generic;

    namespace ConsoleApp
    {
        class Program
        {
            static void Main(string[] args)
            {
                List<Person> persons = new();

                Person person = new("Kelvin", "Hart");

                Console.WriteLine(person.FirstName);

                Person person2 = person with { FirstName = "Michael" };

                Console.WriteLine(person2.FirstName);
            }
        }

        public record Person(string FirstName, string LastName);
    }
  ```



 
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




  # Day 020 - Microsoft C# Yield

  ### What is C# Yield?
  When you use the yield contextual keyword in a statement, you indicate that the method, operator, or get accessor in which it appears is an iterator. Using yield to define an iterator removes the need for an explicit extra class (the class that holds the state for an enumeration, see IEnumerator<T> for an example) when you implement the IEnumerable and IEnumerator pattern for a custom collection type.

  ### Declaration
  ```c#
    yield return <expression>;
    yield break;
  ```
  ### Example
  ```c#
    using System;
    using System.Collections.Generic;

    namespace ConsoleApp
    {
        class Program
        {
            static List<Employee> Employees = new List<Employee>()
            {
                new Employee { Name = "Joshua Clarke", Salary = 19000 },
                new Employee { Name = "Melvin Gayrio", Salary = 18500 },
                new Employee { Name = "Trevor Kelvin", Salary = 12500 },
                new Employee { Name = "Whalte Bryian", Salary = 22500 },
                new Employee { Name = "Elddie Montei", Salary = 28500 },
                new Employee { Name = "Chucks Vinnie", Salary = 13500 }
            };

            static void Main(string[] args)
            {
                Console.WriteLine("========= With Yield ========");

                foreach(var item in FindWithYield())
                {
                    Console.WriteLine($"Name : {item.Name} | Salary : {item.Salary}");
                }

                Console.WriteLine("========= With Yield ========");

                Console.WriteLine("");

                Console.WriteLine("========= Without Yield ========");
                
                foreach (var item in FindWithoutYield())
                {
                    Console.WriteLine($"Name : {item.Name} | Salary : {item.Salary}");
                }

                Console.WriteLine("========= Without Yield ========");
            }

            static IEnumerable<Employee> FindWithYield ()
            {
                foreach(var employee in Employees)
                {
                    if(employee.Salary > 20000)
                    {
                        yield return employee;
                    }
                }
            }

            static IEnumerable<Employee> FindWithoutYield()
            {
                List<Employee> emps = new List<Employee>();

                foreach (var employee in Employees)
                {
                    if (employee.Salary > 20000)
                    {
                        emps.Add(employee);
                    }
                }

                return emps;
            }
        }

        class Employee
        {
            public string Name { get; set; }
            public int Salary { get; set; }
        }
    }
  ```



  
  
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier
  * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/
  * https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/api/system.multicastdelegate?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/api/system.predicate-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/standard/collections/thread-safe/blockingcollection-overview
  * https://www.infoworld.com/article/3227207/how-to-perform-lazy-initialization-in-c.html
  * https://docs.microsoft.com/en-us/dotnet/api/system.lazy-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/api/system.tuple?view=net-5.0
  * https://dotnetcodr.com/2013/08/01/design-patterns-and-practices-in-net-the-observer-pattern
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#discard-pattern
  * https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenany?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenall?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
  * https://docs.microsoft.com/en-us/dotnet/api/system.io.file.create?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types
  * https://docs.microsoft.com/en-us/dotnet/api/system.weakreference?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/yield