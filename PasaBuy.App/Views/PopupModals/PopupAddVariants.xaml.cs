﻿using Newtonsoft.Json;
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
    public partial class PopupAddVariants : PopupPage
    {
        public static string variant_id;
        public PopupAddVariants()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(variant_id))
            {
                try
                {
                    TindaPress.Variant.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ProductVariants.product_id, "", "", variant_id, "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                            if (variants.data.Length > 0)
                            {
                                for (int i = 0; i < variants.data.Length; i++)
                                {
                                    Name.Text = variants.data[i].name;
                                    Description.Text = variants.data[i].info;
                                }
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                        }
                    });
                }
                catch (Exception e)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                }
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
                    if (!string.IsNullOrEmpty(variant_id))
                    {
                        TindaPress.Variant.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, variant_id, ProductVariants.product_id, "", "0", Name.Text, Description.Text, (bool success, string data) =>
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
                    else
                    {
                        TindaPress.Variant.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "0", ProductVariants.product_id, "", "0", Name.Text, Description.Text, (bool success, string data) =>
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
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}