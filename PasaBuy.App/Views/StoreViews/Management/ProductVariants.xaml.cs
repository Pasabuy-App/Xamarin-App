using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductVariants : ContentPage
    {
        public static string product_id;
        public static int LastIndex = 11;
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;
        public ProductVariants()
        {
            InitializeComponent();
            this.BindingContext = new VariantsViewModel();
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
            //TitleName.Text = "Product Name";
            try
            {
                TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", product_id, "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        ProductData product = JsonConvert.DeserializeObject<ProductData>(data);
                        TitleName.Text = product.data[0].product_name;
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

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(100);
            LastIndex = 11;
            isFirstLoad = false;
            Offset = 0;
            VariantsViewModel._variantsList.Clear();
            VariantsViewModel.LoadVariants(product_id);
            pullToRefresh.IsRefreshing = false;
            //new Alert("Variants to Options", " Click Add Model " + PopupAddVariants.type + " " + product_id, "OK");
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    count = 1;
                    bool answer = await DisplayAlert("Delete Variant?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var btn = sender as Grid;
                        TindaPress.Variant.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                VariantsViewModel.LoadVariants(product_id);
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                            }
                        });
                    }
                    await Task.Delay(200);
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private async void Update_Tapped(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                var btn = sender as Grid;
                PopupEditVariants.variant_id = btn.ClassId;
                await PopupNavigation.Instance.PushAsync(new PopupEditVariants());
                await Task.Delay(200);
                count = 0;
            }
        }
    }
}