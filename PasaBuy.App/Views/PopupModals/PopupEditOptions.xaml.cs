using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditOptions : PopupPage
    {
        public static string variant_id;
        public static string product_id;
        public static string option_id;
        public static string option_name;
        public static string option_info;
        public static string option_price;
        public bool isTapped = false;
        public PopupEditOptions()
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(option_id))
            {
                Name.Text = option_name;
                Description.Text = option_info;
                Price.Text = option_price;
            }
        }

        private async void CancelModal(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void OkModal(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text) || !string.IsNullOrWhiteSpace(Price.Text))
                {
                    if (!string.IsNullOrWhiteSpace(option_id))
                    {
                        if (!isTapped)
                        {
                            isTapped = true;
                            Http.TindaPress.Variant.Instance.Update(ProductVariants.product_id, option_id, Name.Text, Description.Text, "false", Price.Text, async (bool success, string data) =>
                            {
                                if (success)
                                {
                                    option_id = string.Empty;
                                    Name.Text = string.Empty;
                                    Description.Text = string.Empty;
                                    Price.Text = string.Empty;
                                    OptionsViewModel.RefreshOptions();
                                    await PopupNavigation.Instance.PopAsync();
                                    isTapped = false;
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    isTapped = false;
                                }
                            });
                        }
                    }
                    else
                    {
                        if (!isTapped)
                        {
                            isTapped = true;
                            Http.TindaPress.Variant.Instance.Insert(ProductVariants.product_id, OptionsViewModel.variant_id, Name.Text, Description.Text, "false", Price.Text, async (bool success, string data) =>
                            {
                                if (success)
                                {
                                    OptionsViewModel.RefreshOptions();
                                    await PopupNavigation.Instance.PopAsync();
                                    isTapped = false;
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    isTapped = false;
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-IU1PUEO", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-IU1PUEO-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-IU1PUEO.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-IU1PUEO-" + err.ToString());
                }
            }
        }
    }
}