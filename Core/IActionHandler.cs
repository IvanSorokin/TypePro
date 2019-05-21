namespace TypePro.Core
{
    public interface IActionHandler
    {
        TypingStateChangedHandler TypingStateChangedHandler { get; }
        
        void PrepareWindow(string[] content);
    }
}