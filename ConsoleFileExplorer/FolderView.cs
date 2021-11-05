using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleFileExplorer
{
    class FolderView
    {
        private int status = 0;
        public string Path { get; private set; }
        private string[] dirs;
        public FolderView(string path)
        {
            Path = path;
            CountDirectoryItems();
            Directory.SetCurrentDirectory(Path);
        }
        public void PrintList()
        {
            //CountDirectoryItems();
            foreach (string dirName in dirs)
            {
                if (dirs[status] == dirName)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Path = dirName;
                }
                if (File.Exists(dirName))
                    Console.WriteLine($"- {System.IO.Path.GetFileName(dirName)}");
                else
                    Console.WriteLine($"# {System.IO.Path.GetFileName(dirName)}");
                Console.ResetColor();
            }
            Console.WriteLine("-------------------------------");
        }
        public void Up()
        {
            if (status > 0)
                status -= 1;
        }
        public void Down()
        {
            if (status < dirs.Length - 1)
                status += 1;
        }
        public void DeleteFile()
        {
            if (File.Exists(Path))
            {
                Console.Clear();
                Console.WriteLine($"Are you sure you want to delete this file\"{Path}\"?\n press y/n");
                ConsoleKeyInfo deleteCheck = Console.ReadKey(true);
                if (deleteCheck.Key == ConsoleKey.Y)
                {
                    File.Delete(Path);
                    Console.WriteLine("File has been deleted successfully!");
                    Console.ReadLine();
                }
                else if (deleteCheck.Key == ConsoleKey.N)
                {
                    Console.WriteLine("File will not be deleted!");
                    Console.ReadLine();
                }
            }
        }
        public void ChangeDirectory()
        {
            if (Directory.Exists(Path))
            {
                Path = dirs[status];
                status = 0;
                CountDirectoryItems();
                Directory.SetCurrentDirectory(Path);
            }
        }
        private void CountDirectoryItems()
        {
            dirs = Directory.GetFileSystemEntries(Path);
        }
        public void FileContent()
        {
            if (File.Exists(Path))
            {
                FileStream fs = File.OpenRead(Path);
                using StreamReader sr = new StreamReader(fs);
                Console.WriteLine(sr.ReadToEnd());
                Console.WriteLine("------------------------------- \n" +
                    "Press any key to return to the list");
                Console.ReadKey();
            }
        }
    }
}
