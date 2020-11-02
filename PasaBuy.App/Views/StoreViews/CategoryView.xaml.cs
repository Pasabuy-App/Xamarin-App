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
        public CategoryView()
        {
            InitializeComponent();
            this.BindingContext = new CategoryViewModel();
            SearchText.TextChanged += OnTextChanged;
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            CategoryViewModel.RefreshCategory(searchBar.Text);
            //searchResults.ItemsSource = DataService.GetSearchResults(searchBar.Text);
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Delete Category?", "Are you sure to delete this?", "Yes", "No");
                if (answer)
                {
                    if (!IsRunning.IsRunning)
                    {
                        IsRunning.IsRunning = true;
                        var btn = sender as Grid;
                        Http.TindaPress.Category.Instance.UpdateDelete(btn.ClassId, "inactive", "", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                CategoryViewModel.RefreshCategory("");
                                IsRunning.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning.IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2D01", "OK");
                IsRunning.IsRunning = false;
            }
        }
    }
}