using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsView : ContentPage
    {
        public static string variant_id;
        public static int LastIndex = 11;
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;
        public OptionsView()
        {
            InitializeComponent();
            this.BindingContext = new VariantsViewModel();

            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
            try
            {
                TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ProductVariants.product_id, "0", "1", variant_id, (bool success, string data) =>
                {
                    if (success)
                    {
                        Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                        TitleName.Text = variants.data[0].name;
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
            await Task.Delay(500);
            LastIndex = 11;
            isFirstLoad = false;
            Offset = 0;
            VariantsViewModel._optionsList.Clear();
            VariantsViewModel.LoadOptions(ProductVariants.product_id, variant_id);
            pullToRefresh.IsRefreshing = false;
        }

        public void BackButtonClicked(object sender, EventArgs e)
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
                    bool answer = await DisplayAlert("Delete Option?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var btn = sender as Grid;
                        TindaPress.Variant.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                VariantsViewModel._optionsList.Clear();
                                VariantsViewModel.LoadOptions(ProductVariants.product_id, variant_id);
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
                //new Alert("Notice to User", btn.ClassId, "Try Again");
                PopupAddVariants.type = "options";
                PopupAddVariants.option_id = btn.ClassId;
                PopupAddVariants.variant_id = variant_id;
                await PopupNavigation.Instance.PushAsync(new PopupAddVariants());
                await Task.Delay(200);
                count = 0;
            }
        }
    }
}