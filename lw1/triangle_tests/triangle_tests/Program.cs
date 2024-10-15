using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace triangle_tests
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    throw new ArgumentException("Usage: triangle_tests.exe <input.txt> <output.txt>");
                }

                StreamReader sr = new StreamReader(args[0]);
                StreamWriter sw = new StreamWriter(args[1]);

                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] testArgs = line.Split(' ');
                    IEnumerable<string> triangleArgs = testArgs.Take(3);
                    string testResult = string.Join(" ", testArgs.Skip(3));
                    string result = "";
                    
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.FileName = "D:\\AStudy\\QualityControl\\lw1\\triangle\\triangle\\bin\\Debug\\triangle.exe";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.StartInfo.RedirectStandardOutput = true;
                        myProcess.StartInfo.Arguments = string.Join(" ", triangleArgs);
                        myProcess.Start();
                        
                        result = myProcess.StandardOutput.ReadLine();
                    }

                    if (result == testResult)
                    {
                        sw.WriteLine("success");
                    }
                    else
                    {
                        sw.WriteLine("error");
                    }
                    
                    line = sr.ReadLine();
                }
                
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}