using System;

namespace KTranslate.MVVM.ViewModels
{
    public interface INonInteractionDialogViewModel
    {
        bool IsClosed { get; }

        event EventHandler DialogIsClosed;
    }
}
