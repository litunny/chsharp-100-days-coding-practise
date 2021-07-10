using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer producer = new Producer();
            try
            {
                producer.Initiate(null, null);

            } catch(ArgumentException e) when (e.Message.Contains("X01"))
            {
                Console.WriteLine("Method Overload one argument wasn't passed");

            } catch(ArgumentException e) when (e.Message.Contains("X02"))
            {
                Console.WriteLine("Method Overload two, with two arguments wasn't passed");
            }

            DetectType("Emmanuel");
            DetectType(1);
        }

        static void DetectType(object obj)
        {
            switch(obj)
            {
                case string name when name == "Emmanuel":
                        Console.WriteLine($"How are you doing {name}");
                    break;

                case int age when age < 18:
                    Console.WriteLine("You're not allow, too young to assimilate!!!");
                    break;

                default:
                    Console.WriteLine("Image Dragon - There's not left to say now (singing)");
                    break;
            }
        }
    }

    class Diamond
    {
        public string type { get; set; }
        public Diamond(string name)
        {
            type = name;
        }
    }
 

    class Producer
    {
        public void Initiate(string args1, string args2)
        {
            if(args1 is null && args1 is null) {
                throw new ArgumentNullException("Error X02. Arguments cannot be null!");
            }
        }
        public void Initiate (string args)
        {
            if (args is null) //This is the use of pattern matching
            {
                throw new ArgumentNullException("Error X01. Argument cannot be null!");
            }
        }
    }
}
