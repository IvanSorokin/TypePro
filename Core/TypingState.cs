namespace TypePro.Core
{
    public delegate void TypingStateChangedHandler(TypingState state);

    public class TypingState
    {
        public TypingState(int errorsCount,
                           int elapsedSeconds,
                           int textLength,
                           int cursorLeft,
                           int cursorTop,
                           bool validSymbolTyped,
                           char typedSymbol,
                           bool isFinished,
                           int validSymbolsTyped)
        {
            ErrorsCount = errorsCount;
            ElapsedSeconds = elapsedSeconds;
            TextLength = textLength;
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
            ValidSymbolTyped = validSymbolTyped;
            TypedSymbol = typedSymbol;
            IsFinished = isFinished;
            ValidSymbolsTyped = validSymbolsTyped;
        }

        public int ErrorsCount { get; }
        public int ElapsedSeconds { get; }
        public int CursorLeft { get; }
        public int CursorTop { get; }
        public int TextLength { get; }
        public int ValidSymbolsTyped { get; }
        public int SymbolsPerMinute => (int) (ValidSymbolsTyped * 1.0 / ElapsedSeconds * 60);
        public bool ValidSymbolTyped { get; }
        public char TypedSymbol { get; }
        public bool IsFinished { get; }
    }
}