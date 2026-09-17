using Microsoft.ML.Data;

namespace KTranslate.Processing.TextProcessing
{
    public class OutputTextPrediction
    {
        [ColumnName("Score")]
        public float Validity;
    }
}
