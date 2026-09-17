using System.Windows.Input;

namespace KTranslate.MVVM.Common
{
    public struct KeyCombination
    {
        public Key Key;
        public ModifierKeys Modifier;

        public override string ToString()
        {
            var keyStr = Key == Key.Oem3 ? "~" : Key.ToString();
            if (Modifier == ModifierKeys.None)
            {
                return keyStr;
            }

            return $"{Modifier}+{keyStr}";
        }
    }
}
