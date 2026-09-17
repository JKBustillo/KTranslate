using System;
using KTranslate.Infrastructure;
using KTranslate.Processing.Interfaces;
using KTranslate.Utils;

namespace KTranslate.Services
{
    public class ChatUITextMediator : IChatTextMediator
    {
        public event EventHandler<TranslatedEventArgs> TextRaised;
        public event EventHandler ClearTextsRaised; 

        public void SendText(string text, bool successful)
        {
            TextRaised?.RaiseOnUIThread(this, new TranslatedEventArgs(text, successful ? TextTypes.Translation : TextTypes.Error));
        }

        public void SendText(string text, TextTypes textType)
        {
            TextRaised?.RaiseOnUIThread(this, new TranslatedEventArgs(text, textType));
        }

        public void ClearTexts()
        {
            ClearTextsRaised?.RaiseOnUIThread(this);
        }
    }
}
