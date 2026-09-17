using System;

namespace KTranslate.MVVM.Common
{
    public interface IAdditionalPanelController
    {
        event EventHandler<bool> PanelStateIsChanged;

        void ClosePanel();
    }
}
