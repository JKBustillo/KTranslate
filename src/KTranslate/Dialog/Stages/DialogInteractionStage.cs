using KTranslate.MVVM.Common;
using KTranslate.MVVM.ViewModels;

namespace KTranslate.Dialog.Stages
{
    public class DialogInteractionStage : ActionInteractionStage
    {
        public DialogInteractionStage(DialogService dialogService, string dialogMessage) : base(dialogService, 
            () => dialogService.ShowDialogAsync(SimpleDialogViewModel.Create(dialogMessage, SimpleDialogTypes.Info)))
        {
        }
    }
}
