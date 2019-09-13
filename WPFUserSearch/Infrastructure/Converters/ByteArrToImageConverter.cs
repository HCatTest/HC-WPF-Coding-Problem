using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Data;

using WPFUserSearch.Infrastructure.Utilities;

namespace WPFUserSearch.Infrastructure.Converters
{
    public class ByteArrToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte[] imageData = (byte[])value;

            return ImageUtilities.ConvertByteArrayToBitmapImage(imageData);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}