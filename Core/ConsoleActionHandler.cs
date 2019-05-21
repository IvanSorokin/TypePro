using System;

namespace TypePro.Core
{
    public class ConsoleActionHandler : IActionHandler
    {
        private bool previousCharWasWrong;
        
        public TypingStateChangedHandler TypingStateChangedHandler
        {
            get
            {
                return state =>
                       {
                           if (state.ValidSymbolTyped)
                           {
                               
                               Console.BackgroundColor = ConsoleColor.Green;
                               Console.ForegroundColor = ConsoleColor.Black;
                               if (previousCharWasWrong)
                               {
                                   Console.BackgroundColor = ConsoleColor.Red;
                                   Console.ForegroundColor = ConsoleColor.White;
                               }

                               Console.Write(state.TypedSymbol);
                               Console.ResetColor();
                               previousCharWasWrong = false;
                           }
                           else
                           {
                               previousCharWasWrong = true;
                           }

                           if (state.IsFinished)
                           {
                               Console.WriteLine();
                               Console.WriteLine($"You did it in {state.ElapsedSeconds} seconds with {state.ErrorsCount} errors");
                               Console.WriteLine($"Symbols per minute: {state.SymbolsPerMinute}");
                               Console.WriteLine();
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