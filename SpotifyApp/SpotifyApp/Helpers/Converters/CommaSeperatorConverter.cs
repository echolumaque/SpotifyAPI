using System;
using System.Globalization;
using Xamarin.Forms;

namespace SpotifyApp.Helpers.Converters
{
    public class CommaSeperatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int raw = (int)value;
            return raw.ToString("N0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
