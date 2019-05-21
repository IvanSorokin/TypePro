using System.Collections.Generic;
using System.Linq;

namespace TypePro
{
    public class ContentPreparer
    {
        public string CutRandomString(string str, int limit)
        {
            var dotPositions = str.Select((x, i) => x == '.' ? i : -1)
                                  .Where(x => x != -1 && x + limit < str.Length)
                                  .ToList();

            var part = str.Skip(dotPositions.PickRandom() + 1).Take(limit);
            
            return string.Join("", part);
        }
        
        public string[] PrepareFromString(string str, int lineWidth, int textLength)
        {
            var parts = str.Replace("\r", " ")
                           .Replace("\n", " ")
                           .Split(' ')
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