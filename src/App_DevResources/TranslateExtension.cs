using System;
using System.Globalization;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Ui
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo ci = new CultureInfo(Startup.CurrentLanguage);
        private static readonly string ResourceId = typeof(AppResources).FullName;
        
       private static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(
            () => new ResourceManager(ResourceId, typeof(TranslateExtension).Assembly));

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = CultureInfo.CurrentCulture;//DependencyService.Get<ILocalize>().GetCurrentCultureInfo(); // TODO:
            }
        }

        // TODO: insert of throw return NOT: FOUND keyworkd and log that
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                throw new ArgumentException(nameof(Text));
            }
            return Text ?? "NoValue";
            var translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
                throw new ArgumentException($"Key '{Text}' was not found in resources '{ResourceId}' for culture '{ci.Name}'.");
            }
            return translation;
        }
        public string Text { get; set; }

    }
}
