using SharpDX.XInput;
using System;
using KTranslate.HotKeys;

namespace KTranslate.Services
{
    public interface IControllerService
    {
        ObservablePipe<Keystroke> EventPipe { get; }

        bool IsListening { get; }

        bool TryChangeListenState(bool enabled);
    }
}
