namespace TypePro
{
    public delegate void TypingStateChangedHandler(TypingState state);

    public class TypingState
    {
        public TypingState(int errorsCount, int elapsedSeconds, int textLength)
        {
            this.ErrorsCount = errorsCount;
            this.ElapsedSeconds = elapsedSeconds;
            this.TextLength = textLength;
        }

        public int ErrorsCount { get; }
        public int ElapsedSeconds { get; }
        public int TextLength { get; }
        public int CharsPerMinute => (int)(TextLength * 1.0 / ElapsedSeconds * 60);
    }
}
