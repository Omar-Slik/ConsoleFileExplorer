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
        private FolderView _currentView;

        public void Run()
        {
            _currentView = new FolderView(".");
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
