using System.Drawing;

namespace KTranslate.Processing
{
    public interface IProcessingService
    {
        bool IsStarted { get; }

        void StartProcessing();

        void ProcessOnce(RectangleF captureArea);

        void StopProcessing();
    }
}
