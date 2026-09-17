namespace KTranslate.Processing.Interfaces
{
    public interface ICapturerFactory
    {
        IScreenCapturer CreateCapturer(bool reliabilityPrioritize);
    }
}
