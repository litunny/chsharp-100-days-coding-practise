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

