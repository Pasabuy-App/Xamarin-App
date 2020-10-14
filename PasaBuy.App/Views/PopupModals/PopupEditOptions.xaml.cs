using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditOptions : PopupPage
    {
        public static string option_id;
        public PopupEditOptions()
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(option_id)) 
            {
                TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ProductVariants.product_id, OptionsViewModel.variant_id, "1", option_id, (bool success, string data) =>
                {
                    if (success)
                    {
                        Options options = JsonConvert.DeserializeObject<Options>(data);
                        for (int i = 0; i < options.data.Length; i++)
                        {
                            Name.Text = options.data[i].name;
                            Description.Text = options.data[i].info;
                            Price.Text = options.data[i].price;
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OkModal(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text) || !string.IsNullOrWhiteSpace(Price.Text))
                {
                    if (!string.IsNullOrWhiteSpace(option_id))
                    {
                        TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, option_id, ProductVariants.product_id, "", Price.Text, Name.Text, Description.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                //new Alert("option_id", "option_id: " + option_id, "OK");
                                option_id = string.Empty;
                                //OptionsViewModel._optionsList.Clear();
                                OptionsViewModel.LoadOptions(ProductVariants.product_id, OptionsViewModel.variant_id);
                                PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                    else
                    {
                        TindaPress.Variant.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, OptionsViewModel.variant_id, ProductVariants.product_id, "0", Price.Text, Name.Text, Description.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                //OptionsViewModel._optionsList.Clear();
                                OptionsViewModel.LoadOptions(ProductVariants.product_id, OptionsViewModel.variant_id);
                                PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");

            }
        }
    }
}