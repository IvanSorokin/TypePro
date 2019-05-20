namespace TypePro
{
    public delegate void TypingStateChangedHandler(TypingState state);

    public class TypingState
    {
        public TypingState(int errorsCount, int elapsedSeconds, int textLength, int cursorLeft, int cursorTop, bool validSymbolTyped, char typedSymbol, bool isFinished)
        {
            ErrorsCount = errorsCount;
            ElapsedSeconds = elapsedSeconds;
            TextLength = textLength;
            CursorLeft = cursorLeft;
            CursorTop = cursorTop;
            ValidSymbolTyped = validSymbolTyped;
            TypedSymbol = typedSymbol;
            IsFinished = isFinished;
        }

        public int ErrorsCount { get; }
        public int ElapsedSeconds { get; }
        public int CursorLeft { get; }
        public int CursorTop { get; }
        public int TextLength { get; }
        public int SymbolsPerMinute => (int) (TextLength * 1.0 / ElapsedSeconds * 60);
        public bool ValidSymbolTyped { get; }
        public char TypedSymbol { get;  }
        public bool IsFinished { get; }
    }
}