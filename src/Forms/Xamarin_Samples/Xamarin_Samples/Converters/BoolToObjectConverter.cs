using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace Xamarin_Samples.Converters
{
    public class BoolToObjectConverter<T> : IValueConverter
    {
        public T TrueObject { set; get; }

        public T FalseObject { set; get; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((T)value).Equals(TrueObject);
        }

        public static string abbreviation(string input)
        {
            string abbreviation = new string(
    input.Split()
          .Where(s => s.Length > 0 && char.IsLetter(s[0]) && char.IsUpper(s[0]))
          .Take(3)
          .Select(s => s[0])
          .ToArray());

            return abbreviation;
        }
    }
}
