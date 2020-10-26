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
        public int isBase = 0;
        public PopupEditVariants()
        {
            InitializeComponent();
            try
            {
                if (!string.IsNullOrEmpty(variant_id))
                {
                    TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ProductVariants.product_id, "0", "1", variant_id, (bool success, string data) =>
                    {
                        if (success)
                        {
                            Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                            for (int i = 0; i < variants.data.Length; i++)
                            {
                                Name.Text = variants.data[i].name;
                                Description.Text = variants.data[i].info;
                                checkBox.IsChecked = variants.data[i].baseprice == "Yes" ? true : false;
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OKModal(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text))
                {
                    //new Alert("Yes", "OK: " + isBase, "OK");
                    TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, variant_id, ProductVariants.product_id, isBase.ToString(), "", Name.Text, Description.Text, (bool success, string data) =>
                    {
                        if (success)
                        {
                            variant_id = string.Empty;
                            VariantsViewModel.LoadVariants(ProductVariants.product_id);
                            PopupNavigation.Instance.PopAsync();
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
                //checkBox.Text = "Checked State";
                isBase = 1;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                //checkBox.Text = "Unchecked State";
                isBase = 0;
            }
        }
    }
}