using System.IO;
using CommandLine;
using TypePro.Core;
using TypePro.Helpers;

namespace TypePro
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(options =>
            {
                string text = null;

                if (options.Random)
                    text = DataProvidingHelper.GetRandomTextFromTheInternet();

                if (options.Text != null)
                    text = options.Text;

                if (options.FilePath != null)
                    text = File.ReadAllText(options.FilePath);

                if (options.FromDb)
                    text = DataProvidingHelper.GetTextFromDb(options.TextLength);

                if (options.FromWiki)
                    text = DataProvidingHelper.GetRandomWikiPage();
                
                if (options.FromQuotes)
                    text = DataProvidingHelper.GetRandomQuotePage();

                text = text ?? DataProvidingHelper.GetDefaultText();

                var content = ContentPreparer.PrepareFromString(text, options.LineWidth, options.TextLength);

                var runner = new TypeProRunner(new ConsoleInputProvider(),
                                               new ConsoleActionHandler(),
                                               content);

                runner.Run();
            });
        }

        
    }
}