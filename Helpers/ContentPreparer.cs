using System.Collections.Generic;
using System.Linq;

namespace TypePro.Helpers
{
    public static class ContentPreparer
    {
        public static string CutRandomString(string str, int limit)
        {
            var endChars = new[] {'.', '?', '!'};
            var endPositions = str.Select((x, i) => endChars.Any(z => z == x) ? i : -1)
                                  .Where(x => x != -1 && x + limit < str.Length)
                                  .ToList();

            var part = str.Skip(endPositions.PickRandom() + 1).Take(limit);
            
            return string.Join("", part);
        }

        private static string ReplaceSymbols(string str)
        {
            var replacements = new List<(string str, string replacement)>
                          {
                              ("—", "-"),
                              ("“", "\""),
                              ("”", "\""),
                              ("\r", " "),
                              ("\n", " ")
                          };
            foreach (var replacement in replacements)
            {
                str = str.Replace(replacement.str, replacement.replacement);
            }

            return str;
        }
        
        public static string[] PrepareFromString(string str, int lineWidth, int textLength)
        {
            var strWithReplacements = ReplaceSymbols(str);
            var parts = strWithReplacements.Split(' ')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(x => x.Trim());
            
            var result = new List<string>();
            var acc = new List<string>();
            var currentTextLength = 0;

            foreach (var part in parts)
            {
                if (currentTextLength + 1 + part.Length >= textLength)
                {
                    var substr = string.Join("", part.Take(textLength - currentTextLength));

                    if (substr.Length + acc.Sum(x => x.Length + 1) <= lineWidth)
                    {
                        if (substr.Any())
                            acc.Add(substr);
                        result.Add(JoinOnSpace(acc));
                    }
                    else
                    {
                        result.Add(JoinOnSpace(acc));
                        if (substr.Any())
                            result.Add(substr);
                    }

                    acc.Clear();
                    break;
                }

                if (acc.Sum(x => x.Length + 1) + part.Length <= lineWidth)
                    acc.Add(part);
                else
                {
                    result.Add(JoinOnSpace(acc));
                    acc.Clear();
                    acc.Add(part);
                }

                currentTextLength += part.Length + 1;
            }
            
            if (acc.Any())
                result.Add(JoinOnSpace(acc));

            return result.ToArray();
        }

        private static string JoinOnSpace(List<string> acc) => string.Join(" ", acc);
    }
}