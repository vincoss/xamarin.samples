using System;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Localization.Resources;

namespace Xamarin_Localization.Extensions
{
    [ContentProperty("Text")]
    public class TranslateResourceExtension : IMarkupExtension
    {
        private static readonly string ResourceId = typeof(AppResources).FullName;

        private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
             () => new ResourceManager(ResourceId, typeof(TranslateExtension).Assembly));

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                throw new ArgumentException(nameof(Text));
            }

            var translation = ResMgr.Value.GetString(Text);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException($"Key '{Text}' was not found in resources '{ResourceId}' for culture '{CultureInfo.CurrentCulture}'.");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
        public string Text { get; set; }
    }
}
