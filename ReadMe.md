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
  
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=net-5.0
  * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier
