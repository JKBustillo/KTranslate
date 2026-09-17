using System;
using Microsoft.Extensions.Logging;
using KTranslate.Infrastructure.Dispatching;
using KTranslate.Infrastructure.Language;
using KTranslate.Translation.Configuration;
using KTranslate.Translation.Deepl;
using KTranslate.Translation.Google;
using KTranslate.Translation.Ollama;
using KTranslate.Translation.Papago;
using KTranslate.Translation.Yandex;

namespace KTranslate.Translation
{
    public class TranslatorFactory
    {
        private readonly LanguageService _languageService;
        private readonly IActionDispatcher _actionDispatcher;
        private readonly ILogger _logger;

        public TranslatorFactory(LanguageService languageService, IActionDispatcher actionDispatcher, ILogger<TranslatorFactory> logger)
        {
            this._languageService = languageService;
            this._actionDispatcher = actionDispatcher;
            this._logger = logger;
        }

        public ITranslator CreateTranslator(TranslationConfiguration translatorConfiguration)
        {
            switch (translatorConfiguration.Translator)
            {
                case Translators.Deepl:
                    return new DeepLTranslator(translatorConfiguration, _languageService, _logger);
                case Translators.Yandex:
                    return new YandexTranslator(translatorConfiguration, _languageService, _actionDispatcher, _logger);
                case Translators.Papago:
                    return new PapagoTranslator(translatorConfiguration, _languageService, _logger);
                case Translators.Ollama:
                    return new OllamaTranslator(translatorConfiguration, _languageService, _logger);
                case Translators.Google:
                    return new GoogleTranslator(translatorConfiguration, _languageService, _logger);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
