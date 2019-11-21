using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Xamarin_Validation.Validation
{
    public static class ValidationExtensions
    {
        public static void ValidateToModel(this IValidator validator, object value, ModelStateDictionary model)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            model.Clear();
            var result = validator.Validate(value);
            foreach (var error in result.Errors)
            {
                model.AddError(error.PropertyName, error.ErrorMessage);
            }
        }


        private static readonly HashSet<char> nremove = new HashSet<char> { '\t', '\n', '\v', '\f', '\r', '\b', '\0' };
        private static readonly HashSet<char> invalidFileNameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

        public static string GetInvalidCharacters(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var chars = (from x in name
                         where invalidFileNameChars.Contains(x)
                         select x).ToArray();

            return new string(chars);
        }

        public static string clean(string name, HashSet<char> invalid)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            if (invalid.Count <= 0)
            {
                invalid = nremove;
            }

            // https://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames
            // https://stackoverflow.com/questions/3253247/how-do-i-detect-non-printable-characters-in-net
            // The set of Unicode character categories containing non-rendering,
            // unknown, or incomplete characters.
            // !! Unicode.Format and Unicode.PrivateUse can NOT be included in
            // !! this set, because they may (private-use) or do (format)
            // !! contain at least *some* rendering characters.
            var nonRenderingCategories = new UnicodeCategory[]
            {
                UnicodeCategory.Control,
                UnicodeCategory.OtherNotAssigned,
                UnicodeCategory.Surrogate
            };

            var sb = new StringBuilder();

            foreach (var c in name)
            {
                // Char.IsWhiteSpace() includes the ASCII whitespace characters that
                // are categorized as control characters. Any other character is
                // printable, unless it falls into the non-rendering categories.
                var isPrintable = Char.IsWhiteSpace(c) || !nonRenderingCategories.Contains(Char.GetUnicodeCategory(c));

                if (isPrintable && invalid.Count > 0)
                {
                    isPrintable = (invalid.Contains(c) == false);
                }

                if (isPrintable)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();

        }
    }
}

// sample
////////////[Theory]
////////////[InlineData("Test", "Test")]
////////////[InlineData("John", "John")]
////////////[InlineData("João", "João")]
////////////[InlineData("タロウ", "タロウ")]
////////////[InlineData("やまだ", "やまだ")]
////////////[InlineData("山田", "山田")]
////////////[InlineData("先生", "先生")]
////////////[InlineData("мыхаыл", "мыхаыл")]
////////////[InlineData("Θεοκλεια", "Θεοκλεια")]
////////////[InlineData("आकाङ्क्षा", "आकाङ्क्षा")]
////////////[InlineData("علاء الدين", "علاء الدين")]
////////////[InlineData("אַבְרָהָם", "אַבְרָהָם")]
////////////[InlineData("മലയാളം", "മലയാളം")]
////////////[InlineData("상", "상")]
////////////[InlineData("D'Addario", "D'Addario")]
////////////[InlineData("John-Doe", "John-Doe")]
////////////[InlineData("P.A.M.", "P.A.M.")]
////////////[InlineData("' --", "' --")]
////////////[InlineData("<xss>", "xss")]
////////////[InlineData("\\", "")]
////////////[InlineData(" ", " ")]
////////////[InlineData("Tricyclopltz^2-Glockenschpiel", "Tricyclopltz^2-Glockenschpiel")]
////////////[InlineData("George Forman the 4th", "George Forman the 4th")]
////////////[InlineData("@#$%^", "@#$%^")]
////////////[InlineData("John W. Saunders, 3rd", "John W. Saunders, 3rd")]
////////////[InlineData("Saint-Louis-du-Ha! Ha!", "Saint-Louis-du-Ha! Ha!")]
////////////[InlineData(" 和訳もあります。]", " 和訳もあります。]")]
////////////[InlineData("田中太郎", "田中太郎")]
////////////public void TestN(string value, string expected)
////////////{
////////////    var str1 = ValidationExtensions.SanitizeFileName(value, new HashSet<char>());
////////////    var inv1 = ValidationExtensions.GetInvalidCharacters(value);
////////////    var str2 = ValidationExtensions.SanitizeFileName(value, new HashSet<char>(inv1));

////////////    Assert.Equal(expected, str2);
////////////}