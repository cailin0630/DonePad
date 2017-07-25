using DonePadClient.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DonePadClient.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class StringToPasswordStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.ToPasswordString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string)?.ToPasswordString();
        }
    }
}