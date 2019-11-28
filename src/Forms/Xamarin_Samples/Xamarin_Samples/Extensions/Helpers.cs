using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Xamarin_Samples.Extensions
{
    public static class Helpers
    {
        public static readonly HashAlgorithm SHA256 = new SHA256Managed();

        public static string GetHashSHA256File(string fileName)
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
       
        public static string GetHashSHA256Text(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            byte[] textData = Encoding.UTF8.GetBytes(value);
            return BitConverter.ToString(SHA256.ComputeHash(textData)).Replace("-", string.Empty);
        }

        /// <summary>
        /// https://exercism.io/tracks/csharp/exercises/acronym/solutions/f1aff07f933d4f4ba92a709b80686ecf
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string Abbreviate(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return null;
            }
            string[] sentance = phrase.Split(new char[] { ' ', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (string word in sentance)
            {
                sb.Append(word[0].ToString().ToUpper());
            }
            return sb.ToString();
        }
    }
}
