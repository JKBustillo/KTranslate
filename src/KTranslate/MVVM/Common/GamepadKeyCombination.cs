using SharpDX.XInput;

namespace KTranslate.MVVM.Common
{
    public struct GamepadKeyCombination
    {
        public GamepadKeyCode Key;

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
