using System;

namespace KTranslate.Infrastructure.Language
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LanguageCodeAttribute : Attribute
    {
        public string LanguageCode { get; set; }
        public LanguageCodeAttribute(string code)
        {
            this.LanguageCode = code;
        }
    }
}
