using System;
using System.Drawing;

namespace KTranslate.Processing.Interfaces
{
    public interface IScreenCapturer : IDisposable
    {
        int CaptureAttempts { get; set; }

        void Initialize();
        byte[] CaptureScreen();
        byte[] CaptureScreen(RectangleF captureArea);
    }
}
