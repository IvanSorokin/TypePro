using System.Collections.Generic;

namespace TypePro
{
    public class WikiResponse
    {
        public class WikiQuery
        {
            public Dictionary<string, WikiPage> Pages;
        }
        
        public class WikiPage
        {
            public string Extract { get; set; }
        }

        public WikiQuery Query { get; set; }
    }
}