using System.Windows;
using KTranslate.MVVM.Common;
using KTranslate.MVVM.ViewModels;

namespace KTranslate.Dialog.Stages
{
    public class DialogQuestionInteractionStage : ConditionalInteractionStage
    {
        public DialogQuestionInteractionStage(DialogService dialogService, string stageQuestion) : base(dialogService, async () =>
                (await dialogService.ShowDialogAsync(SimpleDialogViewModel.Create(stageQuestion, SimpleDialogTypes.Question))) == MessageBoxResult.OK)
        {
        }
    }
}
