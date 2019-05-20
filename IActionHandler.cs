namespace TypePro
{
    public interface IActionHandler
    {
        TypingStateChangedHandler TypingStateChangedHandler { get; }
        
        void PrepareWindow(string[] content);
    }
}