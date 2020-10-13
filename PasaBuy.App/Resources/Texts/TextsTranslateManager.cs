using Plugin.Multilingual;
using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Resources.Texts
{
    public static class TextsTranslateManager
    {
        const string ResourceId = "PasaBuy.App.Resources.Texts.AppResources";

        public static readonly Lazy<ResourceManager> LazyResourceManager =
            new Lazy<ResourceManager>(() =>
                new ResourceManager(ResourceId, typeof(TranslateExtension)
                    .GetTypeInfo().Assembly));

        public static string Translate(string text)
        {
            //Settings.LanguageSelected = CrossMultilingual.Current.CurrentCultureInfo.EnglishName;
            //CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.EnglishName.Contains(Settings.LanguageSelected.ToString()));
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            var translation = LazyResourceManager.Value.GetString(text, ci);
            if (translation == null)
            {
                translation = text;
            }
            return translation;
        }
    }

    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            return TextsTranslateManager.Translate(Text);
        }
    }
}
