using System.Collections.Generic;
using System.Net;

namespace KTranslate.Infrastructure.Dispatching
{
    public class BrowseSiteDispatchResult
    {
        public IEnumerable<Cookie> Cookies { get; set; }

        public string HtmlPage { get; set; }
    }
}
