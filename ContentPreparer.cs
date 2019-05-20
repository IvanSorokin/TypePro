using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypePro
{
    public class ContentPreparer
    {
        public string[] PrepareFromString(string str, int lineWidth, int textLength)
        {
            var parts = str.Replace("\r", " ")
                           .Replace("\n", " ")
                           .Split(' ')
                           .Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(x => x.Trim());
            
            var sb = new StringBuilder();
            var result = new List<string>();
            var currentTextLength = 0;

            foreach (var part in parts)
            {
                if (currentTextLength + part.Length >= textLength)
                {
                    var substr = string.Join("", part.Take(textLength - currentTextLength));

                    if (substr.Length + sb.Length <= lineWidth)
                    {
                        sb.Append(substr);
                        result.Add(sb.ToString());
                    }
                    else
                    {
                        result.Add(sb.ToString());
                        result.Add(substr);
                    }

                    sb.Clear();
                    currentTextLength += substr.Length;
                    break;
                }

                if (sb.Length + part.Length + 1 <= lineWidth)
                    sb.Append(part + ' ');
                else
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(part + ' ');
                }

                currentTextLength += part.Length + 1;
            }

            if (currentTextLength < lineWidth && sb.Length > 0)
                result.Add(sb.ToString());

            return result.Select(x => x.TrimEnd()).ToArray();
        }
    }
}