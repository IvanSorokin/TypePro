using System;

namespace TypePro
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        public TypingStateChangedHandler TypingStateChangedHandler => null;

        public void ConfigureColors()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public void Write(char ch) => Console.Write(ch);

        public void WriteLine(string s) => Console.WriteLine(s);

        public void Clear() => Console.Clear();

        public void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public void HandleResult(TypingState result)
        {
            Console.WriteLine();
            Console.WriteLine($"You did it in {result.ElapsedSeconds} seconds with {result.ErrorsCount} errors");
            Console.WriteLine($"Symbols per minute: {result.SymbolsPerMinute}");
        }

        public void ResetColors() => Console.ResetColor();
    }
}
