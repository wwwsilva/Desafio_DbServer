using Desafio_DBServer.Helpers;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Desafio_DBServer.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var fileName = parameter.ToString();
                return ImageHelper.GetImageResource(fileName);
            }
            catch 
            {
                return ImageHelper.GetImageResource("PokeBall.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
