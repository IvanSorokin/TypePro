using CommandLine;

namespace TypePro
{
    public class Options
    {
        [Option('f', "from-file", Required = false, HelpText = "Path to txt file")]
        public string FilePath { get; set; }

        [Option('t', "from-text", Required = false, HelpText = "Text to work with")]
        public string Text { get; set; }
        
        [Option('d', "from-db", Required = false, HelpText = "Pick text from random txt file from Texts directory")]
        public bool FromDb { get; set; }
        
        [Option('w', "from-wiki", Required = false, HelpText = "Pick text from random wiki page")]
        public bool FromWiki { get; set; }

        [Option('l', "text-length", Required = false, HelpText = "Desirable length of text", Default = 200)]
        public int TextLength { get; set; }
        
        [Option("line-width", Required = false, HelpText = "Desirable length of each line", Default = 80)]
        public int LineWidth { get; set; }

        [Option('r', "random", Required = false, HelpText = "Pick random text from the internet", Default = false)]
        public bool Random { get; set; }
    }
}