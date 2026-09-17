using System.Globalization;
using System.Xml.Serialization;
using Translumo.Utils;

namespace Translumo.Configuration
{
    public class SystemConfiguration : BindableBase
    {
        public static SystemConfiguration Default => new SystemConfiguration()
        {
            ApplicationCulture = "en-US",
            ApplicationTheme = ThemeManager.LightTheme
        };

        public string ApplicationCulture
        {
            get => _applicationCulture;
            set
            {
                SetProperty(ref _applicationCulture, value);
                UpdateSelectedLanguage();
            }
        }

        public string ApplicationTheme
        {
            get => _applicationTheme;
            set
            {
                SetProperty(ref _applicationTheme, value);
                ThemeManager.ChangeAppTheme(_applicationTheme);
            }
        }

        // view-only mirror of ApplicationTheme for the toggle; persisting both would let them disagree on load
        [XmlIgnore]
        public bool DarkThemeEnabled
        {
            get => ApplicationTheme == ThemeManager.DarkTheme;
            set
            {
                ApplicationTheme = value ? ThemeManager.DarkTheme : ThemeManager.LightTheme;
                OnPropertyChanged();
            }
        }

        private string _applicationCulture;
        private string _applicationTheme;

        private void UpdateSelectedLanguage()
        {
            LocalizationManager.ChangeAppCulture(new CultureInfo(ApplicationCulture));
        }
    }
}
