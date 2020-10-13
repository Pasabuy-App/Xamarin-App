using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        public int count = 0;
        public CategoryView()
        {
            InitializeComponent();
        }

        private async void AddCategoryClicked(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                await Task.Delay(200);
                PopupAddCategory.catid = "0";
                await PopupNavigation.Instance.PushAsync(new PopupAddCategory());
                count = 0;
            }
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Delete Category?", "Are you sure to delete this?", "Yes", "No");
                if (answer)
                {
                    var btn = sender as Grid;
                    TindaPress.Category.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                    {
                        if (success)
                        {
                            CategoryViewModel.categoriesList.Clear();
                            CategoryViewModel.LoadData();
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

        private async void Update_Tapped(object sender, EventArgs e)
        {
            var btn = sender as Grid;
            await Task.Delay(200);
            PopupAddCategory.catid = btn.ClassId;
            await PopupNavigation.Instance.PushAsync(new PopupAddCategory());
            //new Alert("Title", btn.ClassId, "OK");
        }
    }
}