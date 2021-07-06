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
  ### References
  * https://docs.microsoft.com/en-us/dotnet/api/system.io.file.create?view=net-5.0