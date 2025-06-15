using Ganss.XSS;
using System.Collections.Generic;

namespace FlightNode.Common.Utility
{
    public class Sanitizer : ISanitizer
    {
        
        public string RemoveAllHtml(string input)
        {
            var emptyList = new List<string>();
            return new HtmlSanitizer(allowedTags: emptyList, allowedAttributes: emptyList, allowedCssProperties: emptyList, allowedSchemes: emptyList)
                .Sanitize(input);
        }

        public string RemoveBadHtml(string input)
        {
            return new HtmlSanitizer()
                .Sanitize(input);
        }
    }
}
