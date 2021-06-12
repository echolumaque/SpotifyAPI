using System;
using System.Globalization;
using Xamarin.Forms;

namespace SpotifyApp.Helpers.Converters
{
    public class EventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
