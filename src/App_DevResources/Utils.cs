using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections;

namespace Ui
{
    // TODO: tests
    public static class Utils
    {
        private static readonly HashAlgorithm SHA256 = new SHA256Managed();

        private static readonly Regex IntExpression = new Regex(@"^(\+|-)?\d+$", RegexOptions.Compiled);
        private static readonly Regex GuidExpression = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
        public static readonly HashSet<char> InvalidFileNameChars = new HashSet<char>(Path.GetInvalidFileNameChars());
        public static readonly HashSet<char> InvalidCharacters = new HashSet<char> { '\t', '\n', '\v', '\f', '\r', '\b', '\0' }; // TODO: add more and see usage and consequences

        public static string SanitizeProjectName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var str2 = SanitizeString(value, new HashSet<char>(InvalidFileNameChars));
            str2 = SanitizeString(str2, new HashSet<char>(InvalidCharacters));
            str2 = ReplaceMultiSpaceWithSingle(str2);
            str2 = str2.Trim().Replace(" ", "-");   // Spaces replace with dash
            str2 = str2.Trim(new[] { '-' });        // Trim start and end dash
            str2 = str2.Trim();                     // All white space
            return str2;
        }

        public static string SanitizeSiteName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var str2 = SanitizeString(value, new HashSet<char>(InvalidFileNameChars));
            str2 = SanitizeString(str2, new HashSet<char>(InvalidCharacters));
            str2 = ReplaceMultiSpaceWithSingle(str2);
            str2 = str2.Trim().Replace(" ", "-");   // Spaces replace with dash
            str2 = str2.Trim(new[] { '-' });        // Trim start and end dash
            str2 = str2.Trim();                     // All white space
            return str2;
        }

        public static string SanitizeTagName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var str2 = SanitizeString(value, new HashSet<char>(InvalidFileNameChars));
            str2 = SanitizeString(str2, new HashSet<char>(InvalidCharacters));
            str2 = ReplaceMultiSpaceWithSingle(str2);
            str2 = str2.Trim().Replace(" ", "-");   // Spaces replace with dash
            str2 = str2.Trim(new[] { '-' });        // Trim start and end dash
            str2 = str2.Trim();                     // All white space
            return str2;
        }

        public static int GetLength(string value)
        {
            if (value == null)
            {
                return 0;
            }
            return value.Length;
        }

        public static string ReplaceMultiSpaceWithSingle(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return Regex.Replace(value, @"\s+", " ");
        }

        public static string GetSha256HashFromString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            byte[] textData = Encoding.UTF8.GetBytes(text);
            return BitConverter.ToString(SHA256.ComputeHash(textData)).Replace("-", string.Empty);
        }

        public static string GetSha256HashFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            using (var stream = new BufferedStream(File.OpenRead(fileName), 100000))
            {
                return BitConverter.ToString(SHA256.ComputeHash(stream)).Replace("-", string.Empty);
            }
        }

        public static string GetFileName()
        {
            return Guid.NewGuid().ToString();
        }

        #region GetGuid

        /// <summary>
        /// Gets the Guid type from specified object.
        /// </summary>
        /// <param name="value">Object to get Guid type.</param>
        /// <param name="defaultValue">Default value if object is invalid.</param>
        /// <returns>Guid type.</returns>
        public static Guid GetGuid(this object value, Guid defaultValue)
        {
            return (!IsGuid(value)) ? defaultValue : new Guid(value.ToString());
        }

        /// <summary>
        /// Check whether specified object is Guid type.
        /// </summary>
        /// <param name="value">Object to check.</param>
        /// <returns>True if is Guid type.</returns>
        private static bool IsGuid(object value)
        {
            return ((value != null) && (value != DBNull.Value)) ? GuidExpression.Match(value.ToString()).Success : false;
        }
        #endregion

        #region GetInteger

        ///<summary>
        /// Gets the Integer value from specified object.
        ///</summary>
        ///<param name="value">Object to get Integer.</param>
        ///<param name="defaultValue">Default value if object is invalid.</param>
        ///<returns>Integer value</returns>
        public static int GetInt32(this object value, int defaultValue = 0)
        {
            return (!IsInteger(value)) ? defaultValue : Convert.ToInt32(value);
        }

        ///<summary>
        /// Gets the Integer value from specified object.
        ///</summary>
        ///<param name="value">Object to get Integer.</param>
        ///<param name="defaultValue">Default value if object is invalid.</param>
        ///<returns>Integer value</returns>
        public static long GetInt64(this object value, int defaultValue = 0)
        {
            return (!IsInteger(value)) ? defaultValue : Convert.ToInt64(value);
        }

        /// <summary>
        /// Checks whether specified object is Integer type.
        /// </summary>
        /// <param name="value">Object to check.</param>
        /// <returns>True if is Integer.</returns>
        private static bool IsInteger(object value)
        {
            return ((value != null) && (value != DBNull.Value)) ? IntExpression.Match(value.ToString()).Success : false;
        }
        #endregion

        public static string SanitizeString(string name, ISet<char> invalid)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            if (invalid.Count <= 0)
            {
                invalid = InvalidCharacters;
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

                // ASCII/Unicode characters 1 through 31
                if (c < 32)
                {
                    isPrintable = false;
                }

                if (isPrintable)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();

        }

        public static string SanitizeHexColor(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            value = value.Trim();
            if (Regex.Match(value, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
            {
                return value;
            }
            return null;
        }

        public static string SanitizeDescription(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var str2 = SanitizeString(value, new HashSet<char>(InvalidFileNameChars));
            str2 = SanitizeString(str2, new HashSet<char>(InvalidCharacters));
            str2 = ReplaceMultiSpaceWithSingle(str2);
            str2 = str2.Trim();
            return str2;
        }

        public static bool FileDelete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            bool flag;
            var attempts = 0;
            Start:

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                flag = true;
            }
            catch
            {
                if (attempts >= 10)
                {
                    throw;
                }
                System.Threading.Thread.Sleep(100);
                attempts++;
                goto Start;
            }

            return flag;
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            int index = 0;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        //public static async void SafeFireAndForget(this Task task,
        //    bool returnToCallingContext,
        //    Action<Exception> onException = null)
        //{
        //    try
        //    {
        //        await task.ConfigureAwait(returnToCallingContext);
        //    }

        //    // if the provided action is not null, catch and
        //    // pass the thrown exception
        //    catch (Exception ex) when (onException != null)
        //    {
        //        onException(ex);
        //    }
        //}

        //public static string WithMaxLength(this string value, int maxLength, string ellipsis = "...", bool keepFullWordAtEnd = true) // TODO: remove if not used
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //    {
        //        return string.Empty;
        //    }
        //    if (value.Length < maxLength)
        //    {
        //        return value;
        //    }
        //    value = value.Substring(0, maxLength);
        //    if (keepFullWordAtEnd)
        //    {
        //        value = value.Substring(0, value.LastIndexOf(' '));
        //    }
        //    return value + ellipsis;
        //}

        //    public static string Abbreviation(string input)
        //    {
        //        string abbreviation = new string(
        //input.Split()
        //      .Where(s => s.Length > 0 && char.IsLetter(s[0]) && char.IsUpper(s[0]))
        //      .Take(3)
        //      .Select(s => s[0])
        //      .ToArray());

        //        return abbreviation;
        //    }

        //public static DateTime? ToLocalDateTime(DateTime? dateTime) // TODO: remove is not used
        //{
        //    if(dateTime == null)
        //    {
        //        return null;
        //    }
        //    return dateTime.Value.ToLocalTime();
        //}

        //// TODO: not used
        //public static double Truncate(this double d)
        //{
        //    return d > 0 ? Math.Floor(d) : -Math.Floor(-d);
        //}


        //// TODO: not used
        //public static void GetRowAndColumnFromIndex(int index, int width, out int r, out int c)
        //{
        //    if (index < 0)
        //    {
        //        throw new ArgumentOutOfRangeException("index");
        //    }
        //    if (width <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("width");
        //    }
        //    r = index % width;
        //    c = index / width;
        //}



        // TODO: remove
        //public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        //{
        //    if (propertyExpression == null)
        //    {
        //        throw new ArgumentNullException("propertyExpression");
        //    }

        //    var body = propertyExpression.Body as MemberExpression;

        //    if (body == null)
        //    {
        //        throw new ArgumentException("Invalid argument", "propertyExpression");
        //    }

        //    var property = body.Member as PropertyInfo;

        //    if (property == null)
        //    {
        //        throw new ArgumentException("Argument is not a property", "propertyExpression");
        //    }
        //    return property.Name;
        //}

        /*
                public static bool DoubleEquals(double left, double right, double acceptableDifference)
        {
            if (acceptableDifference < 0)
            {
                throw new ArgumentOutOfRangeException("acceptableDifference");
            }
            double difference = Math.Abs(left * acceptableDifference);
            return Math.Abs(left - right) <= difference;
        }

        public static T TryParse<T>(string value, TryParseHandler<T> handler) where T : struct
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            T result;
            if (string.IsNullOrEmpty(value) == false && handler(value, out result))
            {
                return result;
            }
            return default(T);
        }

        public static T? TryParseNullable<T>(string value, TryParseHandler<T> handler) where T : struct
        {
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            T result;
            if (handler(value, out result))
            {
                return result;
            }
            return null;
        }

        public delegate bool TryParseHandler<T>(string value, out T result);

             public string GetDeviceId()
        {
            var id = DeviceExtendedProperties.GetValue("DeviceUniqueId");

            //HostInformation.PublisherHostId;
            if (id == null)
            {
                return string.Empty;
            }
            return id.ToString();
        }
     */

    }
}
