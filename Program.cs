﻿using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using CommandLine;

namespace TypePro
{
    partial class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var text = options.Random ? GetRandomTextFromTheInternet() : GetDefaultText();
                var content = new ContentPreparer().PrepareFromString(text, 80, options.TextLength);

                var runner = new TypeProRunner(new ConsoleInputProvider(),
                                                new ConsoleOutputHandler(),
                                                content);

                runner.Run();
            });
        }

        private static string GetDefaultText()
        {
            return "Hello stranger! This application should help you to type fast!";
        }

        private static string GetRandomTextFromTheInternet()
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("http://randomtextgenerator.com/");

                var regex = new Regex(@"\<textarea.*\>(.*)\<\/textarea\>", RegexOptions.Singleline);
                var match = regex.Match(response);

                return match.Groups[1].Value.Replace("\r", "").Replace("\n", "");
            }
        }
    }
}
