using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypePro
{
    public class ContentPreparer
    {
        public string[] PrepareFromString(string str, int lineWidth, int textLength)
        {
            var parts = str.Split(' ');
            var sb = new StringBuilder();
            var result = new List<string>();
            var currentTextLength = 0;

            foreach (var part in parts)
            {
                if (currentTextLength + part.Length >= textLength)
                {
                    result.Add(sb.ToString());
                    result.Add(string.Join("", part.Take(textLength - currentTextLength)));
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

            if (currentTextLength <= lineWidth)
                result.Add(sb.ToString());

            return result.Select(x => x.TrimEnd()).ToArray();
        }
    }
}