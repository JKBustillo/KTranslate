using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using KTranslate.OCR.EasyOCR;
using KTranslate.OCR.Tesseract;
using KTranslate.OCR.WindowsOCR;

namespace KTranslate.OCR.Configuration
{
    [XmlInclude(typeof(EasyOCRConfiguration))]
    [XmlInclude(typeof(WindowsOCRConfiguration))]
    [XmlInclude(typeof(TesseractOCRConfiguration))]
    public abstract class OcrConfiguration : INotifyPropertyChanged
    {
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value == _enabled)
                {
                    return;
                }

                _enabled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _enabled;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
