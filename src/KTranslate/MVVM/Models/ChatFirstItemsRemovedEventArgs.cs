using System;

namespace KTranslate.MVVM.Models
{
    public class ChatFirstItemsRemovedEventArgs : EventArgs
    {
        public int NumberRemoved { get; set; }

        public ChatFirstItemsRemovedEventArgs(int numberRemoved)
        {
            this.NumberRemoved = numberRemoved;
        }
    }
}
