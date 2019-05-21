using System.Diagnostics;
using System.Linq;

namespace TypePro.Core
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
        private int validSymbolsTyped;
        private bool currentKeyIsValid;
        private char lastTypedSymbol;

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
                currentKeyIsValid = false;
                lastTypedSymbol = inputProvider.GetKey();
                if (lastTypedSymbol == content[currentRowIndex][currentLineIndex])
                {
                    currentKeyIsValid = true;
                    validSymbolsTyped++;
                    currentLineIndex++;

                    if (content[currentRowIndex].Length <= currentLineIndex)
                    {
                        currentRowIndex++;
                        currentLineIndex = 0;
                    }
                }
                else
                    currentErrorCount++;

                var isFinished = currentRowIndex == content.Length;
                OnChanged?.Invoke(GetTypingState(isFinished, stopWatch.Elapsed.TotalSeconds));
            }

            stopWatch.Stop();
        }

        private TypingState GetTypingState(bool finished, double elapsedSeconds)
        {
            return new TypingState(currentErrorCount,
                                   elapsedSeconds,
                                   textLength,
                                   currentLineIndex,
                                   currentRowIndex,
                                   currentKeyIsValid,
                                   lastTypedSymbol,
                                   finished,
                                   validSymbolsTyped);
        }
    }
}