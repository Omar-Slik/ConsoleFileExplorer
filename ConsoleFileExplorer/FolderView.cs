using System;
using System.IO;

namespace ConsoleFileExplorer
{
    class FolderView
    {
        private string _path;
        private int _status = 0;
        private string[] _directories;
        public string CurrentFileName { get; private set; }
        public FolderView(string path)
        {
            _path = path;
            Directory.SetCurrentDirectory(_path);
        }
        public void PrintList()
        {
            CountDirectoryItems();
            foreach (string dirName in _directories)
            {
                if (_directories[_status] == dirName)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    CurrentFileName=dirName;
                    //_path = dirName;
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
            if (_status > 0)
                _status -= 1;
        }
        public void Down()
        {
            if (_status < _directories.Length - 1)
                _status += 1;
        }
        public void DeleteFile()
        {
            if (File.Exists(CurrentFileName))
            {
                Console.Clear();
                Console.WriteLine($"Are you sure you want to delete this file?\n\"{CurrentFileName}\"\n press y/n");
                ConsoleKeyInfo deleteCheck = Console.ReadKey(true);
                if (deleteCheck.Key == ConsoleKey.Y)
                {
                    File.Delete(CurrentFileName);
                    Console.WriteLine("File has been deleted successfully!");
                }
                else if (deleteCheck.Key == ConsoleKey.N)
                    Console.WriteLine("File will not be deleted!");                    
                else
                    Console.WriteLine("Wrong answer!");
                Console.WriteLine("------------------------------- \n" +
                    "Press any key to return to the list");
                Console.ReadKey();
            }
        }
        public void ChangeDirectory()
        {
            if (Directory.Exists(CurrentFileName))
            {
                _path = _directories[_status];
                _status = 0;
                CountDirectoryItems();
                Directory.SetCurrentDirectory(_path);
            }
        }
        private void CountDirectoryItems()
        {
            _directories = Directory.GetFileSystemEntries(_path);
        }
        public void FileContent()
        {
            if (File.Exists(CurrentFileName))
            {
                FileStream fs = File.OpenRead(CurrentFileName);
                using StreamReader sr = new StreamReader(fs);
                Console.WriteLine(sr.ReadToEnd());
                Console.WriteLine("------------------------------- \n" +
                    "Press any key to return to the list");
                Console.ReadKey();
            }
        }
    }
}
