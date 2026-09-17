using KTranslate.Infrastructure.Language;
using KTranslate.Utils;

namespace KTranslate.MVVM.Models
{
    public class DisplayLanguage : BindableBase
    {
        public LanguageDescriptor LanguageDescriptor
        {
            get => _languageDescriptor;
            set
            {
                SetProperty(ref _languageDescriptor, value);
            }
        }

        public string DisplayName
        {
            get => _displayName;
            set
            {
                SetProperty(ref _displayName, value);
            }
        }

        private LanguageDescriptor _languageDescriptor;
        private string _displayName;

        public DisplayLanguage(LanguageDescriptor languageDescriptor, string displayName)
        {
            this.LanguageDescriptor = languageDescriptor;
            this.DisplayName = displayName;
        }
    }
}
