using System;
using System.Linq;
using System.Windows;

namespace KTranslate.Utils
{
    public static class ThemeManager
    {
        public const string LightTheme = "Light";
        public const string DarkTheme = "Dark";

        public static event EventHandler ThemeChanged;

        public static string CurrentTheme { get; private set; } = LightTheme;

        public static void ChangeAppTheme(string theme)
        {
            // settings saved before the theme existed map a null in here, and a bad palette path would abort the whole config load
            CurrentTheme = theme == DarkTheme ? DarkTheme : LightTheme;
            theme = CurrentTheme;
            var dictionary = new ResourceDictionary
            {
                Source = new Uri($"Themes/Palette.{theme}.xaml", UriKind.Relative)
            };

            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var oldDictionary = dictionaries.FirstOrDefault(dict => dict.Source?.OriginalString.Contains("Themes/Palette.") ?? false);
            if (oldDictionary == null)
            {
                dictionaries.Add(dictionary);
            }
            else
            {
                // replaced in place: the palette must stay after the Material Design dictionaries it overrides
                var index = dictionaries.IndexOf(oldDictionary);
                dictionaries.Remove(oldDictionary);
                dictionaries.Insert(index, dictionary);
            }

            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
