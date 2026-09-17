using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SharpDX.XInput;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using KTranslate.Configuration;
using KTranslate.Dialog;
using KTranslate.HotKeys;
using KTranslate.Infrastructure.Constants;
using KTranslate.Infrastructure.Dispatching;
using KTranslate.Infrastructure.Encryption;
using KTranslate.Infrastructure.Language;
using KTranslate.Infrastructure.MachineLearning;
using KTranslate.Infrastructure.Python;
using KTranslate.MVVM.Models;
using KTranslate.MVVM.ViewModels;
using KTranslate.OCR;
using KTranslate.OCR.Configuration;
using KTranslate.Processing;
using KTranslate.Processing.Configuration;
using KTranslate.Processing.Interfaces;
using KTranslate.Processing.TextProcessing;
using KTranslate.Services;
using KTranslate.Translation;
using KTranslate.Translation.Configuration;
using KTranslate.TTS;
using KTranslate.Update;
using KTranslate.Utils;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace KTranslate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public App()
        {
            Log.Logger = CreateLogger();

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            this._serviceProvider = services.BuildServiceProvider();
            this._logger = _serviceProvider.GetService<ILogger<App>>();

            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CheckIfPathsIsASCII()
        {
            string pythonPath = Global.PythonPath;

            // Extract non-English characters (non-ASCII)
            string nonAscii = new string(pythonPath.Where(c => c > 127).ToArray());

            if (!string.IsNullOrEmpty(nonAscii))
            {
                // Show native Win32 dialog
                NativeDialog.ShowError(
                    $"KTranslate folder is in a path with non-English letters: \"{nonAscii}\"\n" +
                    "Please move KTranslate folder to a simple path like C:\\KTranslate",
                    "Move KTranslate Folder");

                // Stop the app
                Environment.Exit(1);
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.LogCritical(e.ExceptionObject as Exception, "Unhandled app exception");
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogCritical(e.Exception, "Unhandled app exception");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var configurationStorage = _serviceProvider.GetService<ConfigurationStorage>();
            configurationStorage.SaveConfiguration();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CheckIfPathsIsASCII();

            var configurationStorage = _serviceProvider.GetService<ConfigurationStorage>();
            configurationStorage.LoadConfiguration();

            var chatViewModel = _serviceProvider.GetService<ChatWindowViewModel>();
            var dialogService = _serviceProvider.GetService<DialogService>();
            dialogService.ShowWindowAsync(chatViewModel);

            _serviceProvider.RegisterUIInputController();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(builder => builder.AddSerilog(/*Log.Logger,*/ dispose: true));

            services.AddScoped<SettingsViewModel>();
            services.AddScoped<AppearanceSettingsViewModel>();
            services.AddScoped<HotkeysSettingsViewModel>();
            services.AddScoped<LanguagesSettingsViewModel>();
            services.AddScoped<OcrSettingsViewModel>();

            var chatWindowConfiguration = ChatWindowConfiguration.Default;
            services.AddSingleton<OcrGeneralConfiguration>(OcrGeneralConfiguration.Default);
            services.AddSingleton<TranslationConfiguration>(TranslationConfiguration.Default);
            services.AddSingleton<TtsConfiguration>(TtsConfiguration.Default);
            services.AddSingleton<ChatWindowConfiguration>(chatWindowConfiguration);
            services.AddSingleton<HotKeysConfiguration>(HotKeysConfiguration.Default);
            services.AddSingleton<SystemConfiguration>(SystemConfiguration.Default);
            services.AddSingleton<TextProcessingConfiguration>(chatWindowConfiguration.TextProcessing);

            var chatMediatorInstance = new ChatUITextMediator();
            services.AddSingleton<IChatTextMediator, ChatUITextMediator>(provider => chatMediatorInstance);
            services.AddSingleton<ChatUITextMediator>(chatMediatorInstance);
            services.AddSingleton<ChatWindowViewModel>();
            services.AddSingleton<ChatWindowModel>();
            services.AddSingleton<HotKeysServiceManager>();
            services.AddSingleton<ScreenCaptureConfiguration>();
            services.AddSingleton<DialogService>();
            services.AddSingleton<LanguageService>();
            services.AddSingleton<TextDetectionProvider>();
            services.AddSingleton<IActionDispatcher, InteractionActionDispatcher>();
            services.AddSingleton<TextValidityPredictor>();
            services.AddSingleton<IControllerService, GamepadService>();
            services.AddSingleton<IControllerInputProvider, ControllerInputProvider>();
            services.AddSingleton<ObservablePipe<Keystroke>>(new ObservablePipe<Keystroke>(Application.Current.Dispatcher));
            services.AddSingleton<UpdateManager>();
            services.AddSingleton<IReleasesClient, GithubApiClient>(provider => new GithubApiClient("JKBustillo", "KTranslate"));
            services.AddSingleton<ICapturerFactory, ScreenCapturerFactory>();
            services.AddSingleton<PythonEngineWrapper>();

            services.AddTransient<IProcessingService, TranslationProcessingService>();
            services.AddTransient<OcrEnginesFactory>();
            services.AddTransient<TranslatorFactory>();
            services.AddTransient<TextResultCacheService>();
            services.AddTransient<IPredictor<InputTextPrediction, OutputTextPrediction>, MlPredictor<InputTextPrediction, OutputTextPrediction>>();
            services.AddTransient<IEncryptionService, AesEncryptionService>();
            services.AddTransient<LanguageDescriptorFactory>();
            services.AddTransient<TtsFactory>();


            services.AddConfigurationStorage();
        }

        private Logger CreateLogger()
        {
            var configuration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.File("Logs/log.txt", LogEventLevel.Warning, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: 10);

#if DEBUG
            configuration = configuration.WriteTo.File("Logs/trace.txt", LogEventLevel.Verbose, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}");
#endif

            return configuration.CreateLogger();
        }

    }

    public static class NativeDialog
    {
        private const int MB_OK = 0x0;
        private const int MB_ICONERROR = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        public static void ShowError(string message, string title = "Error")
        {
            // hWnd = IntPtr.Zero means no owner window
            MessageBoxW(IntPtr.Zero, message, title, MB_OK | MB_ICONERROR);
        }
    }
}
