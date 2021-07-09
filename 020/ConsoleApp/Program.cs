using System;
using System.Collections.Generic;
using System.Linq;

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
