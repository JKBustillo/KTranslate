using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KTranslate.Configuration;
using KTranslate.HotKeys;
using KTranslate.Infrastructure.Encryption;
using KTranslate.OCR.Configuration;
using KTranslate.Translation.Configuration;
using KTranslate.TTS;

namespace KTranslate.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigurationStorage(this ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ConfigurationStorage>(sc =>
            {
                var confStorage = new ConfigurationStorage(sc, sc.GetService<IEncryptionService>(), sc.GetService<ILogger<ConfigurationStorage>>());
                confStorage.RegisterConfiguration<ChatWindowConfiguration>();
                confStorage.RegisterConfiguration<OcrGeneralConfiguration>();
                confStorage.RegisterConfiguration<TranslationConfiguration>();
                confStorage.RegisterConfiguration<TtsConfiguration>();
                confStorage.RegisterConfiguration<SystemConfiguration>();
                confStorage.RegisterConfiguration<HotKeysConfiguration>();

                return confStorage;
            });
        }
    }
}
