using System.Net;

namespace KTranslate.Infrastructure.Dispatching
{
    public class BrowseSiteDispatchArg
    {
        public WebProxy Proxy { get; set; }

        public string TargetUrl { get; set; }

        public string SourceUrl { get; set; }
    }
}
