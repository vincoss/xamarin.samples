using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Xamarin_Samples.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.Default;
            }
            var str = value.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return Color.Default;
            }
            return Color.FromHex(str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Color.Default.ToHex();
            }
            var color = (Color)value;
            if(color == null)
            {
                return Color.Default.ToHex();
            }
            return color.ToHex();
        }
    }
}
