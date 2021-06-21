using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            Action action = delegate () { Console.WriteLine("Method Fired!!!"); };

            Action<string> action1 = delegate (string value) { Console.WriteLine(value); };

            Action<string, string> action2 = (string value1, string value2) => { Console.WriteLine($"{value1} and {value2}"); };

            action();
            action1("Value One");
            action2("Value One" , "Value Two");

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
            return new UserAccount("Michel Clarke", "Sterling Bank", "038230239", 120.00);
        }

        public static UserAccount GetMyUserAccount ()
        {
            return new UserAccount("John Doe", "Sterling Bank", "003829393", 90.00);
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