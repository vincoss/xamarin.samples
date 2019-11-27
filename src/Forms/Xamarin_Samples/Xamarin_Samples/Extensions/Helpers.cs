using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Samples.Extensions
{
    public static class Helpers
    {
        public static string WithMaxLength(this string value, int maxLength, string ellipsis = "...", bool keepFullWordAtEnd = true) // TODO: remove if not used
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            if (value.Length < maxLength)
            {
                return value;
            }
            value = value.Substring(0, maxLength);
            if (keepFullWordAtEnd)
            {
                value = value.Substring(0, value.LastIndexOf(' '));
            }
            return value + ellipsis;
        }

        public static string Abbreviation(string input)
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
