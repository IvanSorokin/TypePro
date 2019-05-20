using System;

namespace TypePro
{
    public class ConsoleActionHandler : IActionHandler
    {
        public TypingStateChangedHandler TypingStateChangedHandler
        {
            get
            {
                return state =>
                       {
                           if (state.ValidSymbolTyped)
                           {
                               Console.BackgroundColor = ConsoleColor.Yellow;
                               Console.ForegroundColor = ConsoleColor.Black;
                               Console.Write(state.TypedSymbol);
                               Console.ResetColor();
                           }

                           if (state.IsFinished)
                           {
                               Console.WriteLine();
                               Console.WriteLine($"You did it in {state.ElapsedSeconds} seconds with {state.ErrorsCount} errors");
                               Console.WriteLine($"Symbols per minute: {state.SymbolsPerMinute}");
                           }
                           else
                               Console.SetCursorPosition(state.CursorLeft, state.CursorTop);
                       };
            }
        }

        public void PrepareWindow(string[] content)
        {
            Console.Clear();
            foreach (var str in content)
                Console.WriteLine(str);
            Console.SetCursorPosition(0, 0);
        }
    }
}