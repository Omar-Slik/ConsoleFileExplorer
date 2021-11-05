using System;
using System.IO;
using System.Reflection;


namespace ConsoleFileExplorer
{
    class ConsoleExplorer
    {
        public ViewState _viewState = ViewState.List;
        private FolderView folderView = new(Path.GetDirectoryName
            (Assembly.GetExecutingAssembly().Location));
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (_viewState)
                {
                    case ViewState.List:
                        ShowList();
                        break;
                    case ViewState.FileView:
                        ShowFile();
                        break;
                    case ViewState.CreateFile:
                        CreateFile();
                        break;
                }
            }
        }
        private void ShowList()
        {
            folderView.PrintList();
            InputKey();
        }
        private void ShowFile()
        {
            folderView.FileContent();
            _viewState = ViewState.List;
        }
        private void InputKey()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.Backspace:
                    folderView = new FolderView(Directory.GetParent
                    (Directory.GetCurrentDirectory()).FullName);
                    break;
                case ConsoleKey.Enter:
                    folderView.ChangeDirectory();
                    break;
                case ConsoleKey.Spacebar:
                    _viewState = ViewState.FileView;
                    break;
                case ConsoleKey.UpArrow:
                    folderView.Up();
                    break;
                case ConsoleKey.DownArrow:
                    folderView.Down();
                    break;
                case ConsoleKey.C:
                    _viewState = ViewState.CreateFile;
                    break;
                case ConsoleKey.D:
                    DeleteFile();                   
                    break;
            }
        }
        private void CreateFile()
        {
            Console.WriteLine("You are creating a new file.\n" +
                "Please enter the name of the file");
            string NewFileName = Console.ReadLine() + ".txt";
            Console.WriteLine("-------------------------------\n" +
                "Write a text in your file." +
                "\nWhen done, press enter");
            string input = "My text";
            FileStream fs = File.OpenWrite(NewFileName);
            using (StreamWriter sw = new(fs))
            {
                while (input != "")
                {
                    input = Console.ReadLine();
                    sw.WriteLine(input);
                }
            }            
            _viewState = ViewState.List;
        }
        private void DeleteFile()
        {
            folderView.DeleteFile();
            _viewState = ViewState.List;
        }
    }
}
