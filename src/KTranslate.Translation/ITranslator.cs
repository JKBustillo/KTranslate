using System.Threading.Tasks;

namespace KTranslate.Translation
{
    public interface ITranslator
    {
        Task<string> TranslateTextAsync(string sourceText);
    }
}
