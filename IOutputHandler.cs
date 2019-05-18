namespace TypePro
{
    public interface IOutputHandler
    {
        void Configure();

        void Write(char ch);

        void WriteLine(string s);

        void Clear();

        void SetCursorPosition(int left, int top);

        void HandleResult(TypingState result);

        void Reset();

        TypingStateChangedHandler PrintingStateChangedHandler { get; }
    }
}
