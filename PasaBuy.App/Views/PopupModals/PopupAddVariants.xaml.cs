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
    public partial class PopupAddVariants : PopupPage
    {
        public static string option_id;
        public static string variant_id;
        public static string type;
        public PopupAddVariants()
        {
            InitializeComponent();
            try
            {
                if (type == "variants")
                {
                    Name.Placeholder = "Variant Name";
                    Description.Placeholder = "(Optional) Variant Description";
                    //new Alert(ProductVariants.product_id, "", variant_id);
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
                                }
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                            }
                        });
                    }
                }
                if (type == "options")
                {
                    Name.Placeholder = "Option Name";
                    Description.Placeholder = "(Optional) Option Description";
                    //new Alert(ProductVariants.product_id, "option_id: " + option_id, variant_id);
                    if (!string.IsNullOrEmpty(option_id))
                    {
                        TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ProductVariants.product_id, variant_id, "1", option_id, (bool success, string data) =>
                        {
                            if (success)
                            {
                                Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                                for (int i = 0; i < variants.data.Length; i++)
                                {
                                    Name.Text = variants.data[i].name;
                                    Description.Text = variants.data[i].info;
                                }
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                            }
                        });
                    }
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
                    if (type == "variants")
                    {
                        if (!string.IsNullOrEmpty(variant_id))
                        {
                            //new Alert("variant_id", "variant_id: " + variant_id, "OK");
                            TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, variant_id, ProductVariants.product_id, "", "", Name.Text, Description.Text, (bool success, string data) =>
                            {
                                if (success)
                                {
                                    variant_id = string.Empty;
                                    VariantsViewModel._variantsList.Clear();
                                    VariantsViewModel.LoadVariants(ProductVariants.product_id);
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
                            //new Alert("variant_id", "variant_id: insert", "OK");
                            // set base price by user 1 or 0, yes or no
                            TindaPress.Variant.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "0", ProductVariants.product_id, "0", "", Name.Text, Description.Text, (bool success, string data) =>
                            {
                                if (success)
                                {
                                    VariantsViewModel._variantsList.Clear();
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
                    if (type == "options")
                    {
                        //new Alert("option_id", "option_id: " + option_id, "OK");
                        if (!string.IsNullOrEmpty(option_id))
                        {
                            TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, option_id, ProductVariants.product_id, "", "", Name.Text, Description.Text, (bool success, string data) =>
                            {
                                if (success)
                                {
                                    //new Alert("option_id", "option_id: " + option_id, "OK");
                                    option_id = string.Empty;
                                    VariantsViewModel._optionsList.Clear();
                                    VariantsViewModel.LoadOptions(ProductVariants.product_id, OptionsView.variant_id);
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
                            TindaPress.Variant.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, OptionsView.variant_id, ProductVariants.product_id, "0", "", Name.Text, Description.Text, (bool success, string data) =>
                            {
                                if (success)
                                {
                                    VariantsViewModel._optionsList.Clear();
                                    VariantsViewModel.LoadOptions(ProductVariants.product_id, OptionsView.variant_id);
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
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}