using System;
using System.Globalization;
using Xamarin.Forms;

namespace Desafio_DBServer.Converters
{
    public class BooleanInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var valor = value as bool?;

                //Se for null, inverte para true
                if (valor.HasValue == false)
                    return true;

                if (valor.Value) //Se for true, inverte para false
                    return false;
                else //Se for false, inverte para true
                    return true;
            }
            catch { }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
