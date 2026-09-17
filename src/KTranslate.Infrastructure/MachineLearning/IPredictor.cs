using System;

namespace KTranslate.Infrastructure.MachineLearning
{
    public interface IPredictor<TInput, TOutput> : IDisposable
    {
        bool Loaded { get; }

        void LoadModel(string path);
        void UnloadModel();
        TOutput PredictResult(TInput input);
    }
}
