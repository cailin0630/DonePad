using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DonePadClient.Extensions;

namespace DonePadClient.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class StringToPasswordStringConverter:IValueConverter
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
