using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using CommandLine;
using Newtonsoft.Json;

namespace TypePro
{
    class Program
    {
        private static readonly ContentPreparer contentPreparer = new ContentPreparer();

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                string text = null;

                if (options.Random)
                    text = GetRandomTextFromTheInternet();

                if (options.Text != null)
                    text = options.Text;

                if (options.FilePath != null)
                    text = File.ReadAllText(options.FilePath);

                if (options.FromDb)
                    text = GetTextFromDb(options.TextLength);

                if (options.FromWiki)
                    text = GetRandomWikiPage();

                text = text ?? GetDefaultText();

                var content = contentPreparer.PrepareFromString(text, options.LineWidth, options.TextLength);

                var runner = new TypeProRunner(new ConsoleInputProvider(),
                                               new ConsoleActionHandler(),
                                               content);

                runner.Run();
            });
        }

        private static string GetTextFromDb(int limit)
        {
            var paths = Directory.EnumerateFiles("Texts").ToList();
            var randomPath = paths.PickRandom();
            var str = File.ReadAllText(randomPath);

            return contentPreparer.CutRandomString(str, limit);
        }

        private static string GetDefaultText()
        {
            return "Hello stranger! This application should help you to type fast! Call --help for options.";
        }

        private static string GetRandomWikiPage()
        {
            using (var client = new WebClient())
            {
                var url = "https://en.wikipedia.org/w/api.php?action=query&generator=random&grnnamespace=0&prop=extracts&exchars=1000&format=json&explaintext";
                var response = client.DownloadString(url);
                var obj = JsonConvert.DeserializeObject<WikiResponse>(response);

                return obj.Query.Pages.First().Value.Extract;
            }
        }

        private static string GetRandomTextFromTheInternet()
        {
            using (var client = new WebClient())
            {
                var response = client.DownloadString("https://www.online-toolz.com/tools/random-text-generator.php");

                var regex = new Regex("<textarea(.*?)>(.*?)</textarea>", RegexOptions.Singleline);
                var match = regex.Match(response);

                return match.Groups[2].Value;
            }
        }
    }
}