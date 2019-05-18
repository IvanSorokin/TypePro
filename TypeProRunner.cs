using System.Diagnostics;
using System.Linq;

namespace TypePro
{
    public class TypeProRunner
    {
        private readonly IInputProvider inputProvider;
        private readonly IOutputHandler outputHandler;
        private readonly string[] content;
        private readonly int textLength;
        private readonly Stopwatch stopWatch = new Stopwatch();
        private int currentLineIndex;
        private int currentRowIndex;
        private int currentErrorCount;


        public TypeProRunner(IInputProvider inputProvider, IOutputHandler outputHandler, string[] content)
        {
            this.inputProvider = inputProvider;
            this.content = content;
            this.outputHandler = outputHandler;
            this.textLength = content.Select(x => x.Length).Sum();
            if (outputHandler.PrintingStateChangedHandler != null)
                OnChanged += outputHandler.PrintingStateChangedHandler;
        }

        public event TypingStateChangedHandler OnChanged;

        public void Run()
        {
            outputHandler.Clear();
            foreach (var str in content)
                outputHandler.WriteLine(str);
            outputHandler.SetCursorPosition(0, 0);

            outputHandler.Configure();

            stopWatch.Start();

            while (currentRowIndex < content.Length)
            {
                var key = inputProvider.GetKey();
                if (key == content[currentRowIndex][currentLineIndex])
                {
                    outputHandler.Write(key);
                    currentLineIndex++;

                    if (content[currentRowIndex].Length == currentLineIndex)
                    {
                        currentRowIndex++;
                        currentLineIndex = 0;
                        outputHandler.SetCursorPosition(currentLineIndex, currentRowIndex);
                    }
                }
                else
                    currentErrorCount++;

                OnChanged?.Invoke(GetPrintingState());
            }

            outputHandler.Reset();
            stopWatch.Stop();

            outputHandler.HandleResult(GetPrintingState());
        }

        private TypingState GetPrintingState()
        {
            return new TypingState(currentErrorCount,
                                     stopWatch.Elapsed.Seconds,
                                     textLength);
        }
    }
}