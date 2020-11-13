using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsView : ContentPage
    {
        //public static string variant_id;
        public static int LastIndex = 11;
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;
        public static string title;
        public OptionsView()
        {
            InitializeComponent();
            this.BindingContext = new OptionsViewModel();
            TitleName.Text = title;
            xAdd.IsEnabled = false;
            CheckPermission();
        }

        public void CheckPermission()
        {
            bool edit = false;
            bool delete = false;
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_variant")
                {
                    edit = true;
                }
                if (per.action == "delete_variant")
                {
                    delete = true;
                }
            }
            if (!delete && !edit)
            {
                listView.AllowSwiping = false;
            }
            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "add_variant"))
            {
                xAdd.IsEnabled = true;
            }
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        /*private async void Delete_Tapped(object sender, EventArgs e)
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
                                //OptionsViewModel.LoadOptions(ProductVariants.product_id, OptionsViewModel.variant_id);
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
                *//*PopupAddVariants.type = "options";
                PopupAddVariants.variant_id = variant_id;*//*
                PopupEditOptions.option_id = btn.ClassId;
                await PopupNavigation.Instance.PushAsync(new PopupEditOptions());
                await Task.Delay(200);
                count = 0;
                //new Alert("Variants to Options", " Click Add Model " + btn.ClassId + " " + ProductVariants.product_id, "OK");
            }
        }*/
    }
}