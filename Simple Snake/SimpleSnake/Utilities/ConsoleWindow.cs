namespace SimpleSnake.Utilities
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    public static class ConsoleWindow
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;

        public static void CustomizeConsole()
        {
            Console.OutputEncoding = Encoding.Unicode;
#pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
#pragma warning restore CA1416 // Validate platform compatibility
            ShowWindow(ThisConsole, MAXIMIZE);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;
        }
    }
}
