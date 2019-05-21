using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TypePro.Helpers
{
    public static class DataProvidingHelper
    {
        public static string GetTextFromDb(int limit)
        {
            var paths = Directory.EnumerateFiles("Texts").ToList();
            var randomPath = paths.PickRandom();
            var str = File.ReadAllText(randomPath);

            return ContentPreparer.CutRandomString(str, limit);
        }

        public static string GetDefaultText()
        {
            return "Hello stranger! This application should help you to type fast! Call --help for options.";
        }

        public static string GetRandomQuotePage()
        {
            using (var client = new WebClient())
            {
                var url = "http://quotesondesign.com/api/3.0/api-3.0.json?no_cache=%22%20+%20Math.floor(Math.random()*24000)";
                var response = client.DownloadString(url);
                var obj = JsonConvert.DeserializeObject<QuoteResponse>(response);

                return obj.Quote;
            }
        }

        public static string GetRandomWikiPage()
        {
            using (var client = new WebClient())
            {
                var url = "https://en.wikipedia.org/w/api.php?action=query&generator=random&grnnamespace=0&prop=extracts&exchars=1000&format=json&explaintext";
                var response = client.DownloadString(url);
                var obj = JsonConvert.DeserializeObject<WikiResponse>(response);

                return obj.Query.Pages.First().Value.Extract;
            }
        }

        public static string GetRandomTextFromTheInternet()
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