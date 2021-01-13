using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Xamarin_Forms_WebView.Services
{
    public static class SampleExtensions
    {
        public static string GetValueEmbeddedResource(Assembly assembly, string path)
        {
            using (var stream = assembly.GetManifestResourceStream(path))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
