using System.Diagnostics;
using System.Linq;

namespace TypePro
{
    public class TypeProRunner
    {
        private readonly string[] content;
        private readonly IInputProvider inputProvider;
        private readonly IActionHandler actionHandler;
        private readonly int textLength;
        private int currentErrorCount;
        private int currentLineIndex;
        private int currentRowIndex;

        public TypeProRunner(IInputProvider inputProvider, IActionHandler actionHandler, string[] content)
        {
            this.inputProvider = inputProvider;
            this.content = content;
            this.actionHandler = actionHandler;
            textLength = content.Select(x => x.Length).Sum();
            if (actionHandler.TypingStateChangedHandler != null)
                OnChanged += actionHandler.TypingStateChangedHandler;
        }

        public event TypingStateChangedHandler OnChanged;

        public void Run()
        {
            actionHandler.PrepareWindow(content);
            var stopWatch = Stopwatch.StartNew();

            while (currentRowIndex < content.Length)
            {
                var keyIsValid = false;
                var key = inputProvider.GetKey();
                if (key == content[currentRowIndex][currentLineIndex])
                {
                    keyIsValid = true;
                    currentLineIndex++;

                    if (content[currentRowIndex].Length <= currentLineIndex)
                    {
                        currentRowIndex++;
                        currentLineIndex = 0;
                    }
                }
                else
                    currentErrorCount++;

                OnChanged?.Invoke(GetTypingState(currentLineIndex, currentRowIndex, keyIsValid, key, false, (int)stopWatch.Elapsed.TotalSeconds));
            }

            OnChanged?.Invoke(GetTypingState(currentLineIndex, currentRowIndex, false, ' ', true, (int)stopWatch.Elapsed.TotalSeconds));
            
            stopWatch.Stop();
        }

        private TypingState GetTypingState(int cursorLeft, int cursorTop, bool keyIsValid, char typedSymbol, bool finished, int elapsedSeconds)
        {
            return new TypingState(currentErrorCount,
                                   elapsedSeconds,
                                   textLength,
                                   cursorLeft,
                                   cursorTop,
                                   keyIsValid,
                                   typedSymbol,
                                   finished);
        }
    }
}