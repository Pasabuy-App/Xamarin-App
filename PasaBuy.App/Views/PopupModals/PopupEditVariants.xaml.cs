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
    public partial class PopupEditVariants : PopupPage
    {
        public static string variant_id;
        public static string variant_name;
        public static string variant_info;
        public static string variant_required;
        public string isBase = "false";
        public bool isTapped = false;
        public PopupEditVariants()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(variant_id))
            {
                Name.Text = variant_name;
                Description.Text = variant_info;
                checkBox.IsChecked = variant_required == "true" ? true : false;
            }
        }

        private async void CancelModal(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void OKModal(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text))
                {
                    if (!isTapped)
                    {
                        isTapped = true;
                        Http.TindaPress.Variant.Instance.Update(ProductVariants.product_id, variant_id, Name.Text, Description.Text, isBase, "0.00", async (bool success, string data) =>
                        {
                            if (success)
                            {
                                variant_id = string.Empty;
                                VariantsViewModel.RefreshVariants();
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-U1PUEV", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-U1PUEV-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-U1PUEV.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-U1PUEV-" + err.ToString());
                }
            }
        }

        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                isBase = "true";
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                isBase = "false";
            }
        }
    }
}