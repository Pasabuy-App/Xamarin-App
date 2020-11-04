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
        public int isBase = 0;
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
            await Navigation.PopModalAsync();
        }

        private void OKModal(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text))
                {
                    TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, variant_id, ProductVariants.product_id, isBase.ToString(), "", Name.Text, Description.Text, async (bool success, string data) =>
                    {
                        if (success)
                        {
                            variant_id = string.Empty;
                            VariantsViewModel.RefreshVariants();
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                isBase = 1;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                isBase = 0;
            }
        }
    }
}