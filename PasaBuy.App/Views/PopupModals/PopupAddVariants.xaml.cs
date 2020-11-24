using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddVariants : PopupPage
    {
        public string isBase = "false";
        public bool isTapped = false;
        public PopupAddVariants()
        {
            InitializeComponent();
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
                        Http.TindaPress.Variant.Instance.Insert(ProductVariants.product_id, "", Name.Text, Description.Text, isBase, "0.00", async (bool success, string data) =>
                        {
                            if (success)
                            {
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
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-I1PUAV", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-I1PUAV-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-I1PUAV.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-I1PUAV-" + err.ToString());
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