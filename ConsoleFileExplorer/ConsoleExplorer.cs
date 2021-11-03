using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFileExplorer
{
    class ConsoleExplorer
    {
        public void Run()
        {                       
            while (true)
            {
                string[] dirs = Directory.GetFileSystemEntries(".");
                foreach (string dirName in dirs)
                {
                    if (File.Exists(dirName))
                    {
                        Console.WriteLine($"- {Path.GetFileName(dirName)}");
                    }
                    else
                    {
                        Console.WriteLine($"# {Path.GetFileName(dirName)}");
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
