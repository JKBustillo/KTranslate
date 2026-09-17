using System.Net;
using KTranslate.Translation.Configuration;

namespace KTranslate.Translation.Papago
{
    public sealed class PapagoContainer : TranslationContainer
    {
        public PapagoReaderProxy Reader { get; set; }
        public string Guid { get; set; }
        public string AuthKey { get; set; }

        public PapagoContainer(Proxy proxy = null, bool isPrimary = false) : base(proxy, isPrimary)
        {
            Reader = new PapagoReaderProxy(proxy);
        }

        public override void Reset()
        {
            base.Reset();
            Guid = string.Empty;
            Reader.HttpReader.Cookies = new CookieContainer();
        }
    }
}
