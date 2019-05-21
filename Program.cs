﻿using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using CommandLine;

namespace TypePro
{
    class Program
    {
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

                text = text ?? GetDefaultText();

                var content = new ContentPreparer().PrepareFromString(text, options.LineWidth, options.TextLength);

                var runner = new TypeProRunner(new ConsoleInputProvider(),
                                               new ConsoleActionHandler(),
                                               content);

                runner.Run();
            });
        }

        private static string GetDefaultText()
        {
            return "Hello stranger! This application should help you to type fast! Call --help for options.";
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