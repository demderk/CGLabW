using System;
using OpenTK;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL4;

namespace CGLab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("e");
            MainWindow mainWindow = new MainWindow();

            mainWindow.Run();
        }
    }
}
