using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using KTranslate.Infrastructure.Language;
using KTranslate.OCR.EasyOCR;
using KTranslate.OCR.Tesseract;
using KTranslate.OCR.WindowsOCR;
using KTranslate.Utils.Extensions;

namespace KTranslate.OCR.Configuration
{
    public class OcrGeneralConfiguration : INotifyPropertyChanged
    {
        public static OcrGeneralConfiguration Default => new OcrGeneralConfiguration()
        {
            OcrConfigurations = new OcrConfiguration[]
                { new EasyOCRConfiguration(), new WindowsOCRConfiguration(), new TesseractOCRConfiguration() },
        };

        public OcrConfiguration[] OcrConfigurations
        {
            get => _ocrConfigurations;
            set
            {
                if (value != _ocrConfigurations)
                {
                    _ocrConfigurations?.ForEach(conf => conf.PropertyChanged -= OcrConfigurationOnPropertyChanged);
                    value?.ForEach(conf => conf.PropertyChanged += OcrConfigurationOnPropertyChanged);
                }

                _ocrConfigurations = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private OcrConfiguration[] _ocrConfigurations;

        public TConfiguration GetConfiguration<TConfiguration>()
            where TConfiguration : OcrConfiguration
        {
            return (TConfiguration) OcrConfigurations.FirstOrDefault(conf => conf.GetType() == typeof(TConfiguration));
        }

        private void OcrConfigurationOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OcrConfigurations));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
