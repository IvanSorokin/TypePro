using CommandLine;

namespace TypePro
{
    partial class Program
    {
        public class Options
        {
            [Option('f', "from-file", Required = false, HelpText = "Path to txt file")]
            public string FilePath { get; set; }

            [Option('l', "text-length", Required = false, HelpText = "Desirable length of text", Default = 200)]
            public int TextLength { get; set; }

            [Option('r', "random", Required = false, HelpText = "Pick random text from the internet", Default = false)]
            public bool Random { get; set; }
        }
    }
}
