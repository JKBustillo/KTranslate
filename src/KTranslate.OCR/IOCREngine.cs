using KTranslate.Infrastructure.Language;

namespace KTranslate.OCR
{
    public interface IOCREngine
    {
        byte PrimaryPriority { get; }
        bool SecondaryPrimaryCheck { get; }
        int Confidence { get; }
        Languages DetectionLanguage { get; }

        string[] GetTextLines(byte[] image);
    }
}
