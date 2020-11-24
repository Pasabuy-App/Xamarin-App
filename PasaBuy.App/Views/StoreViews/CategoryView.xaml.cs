using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Linq;
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
            //SearchText.TextChanged += OnTextChanged;
            SearchText.SearchButtonPressed += SearchButtonPress;
            xAdd.IsEnabled = false;
            CheckPermission();
        }

        public void CheckPermission()
        {
            bool edit = false;
            bool delete = false;
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_category")
                {
                    edit = true;
                }
                if (per.action == "delete_category")
                {
                    delete = true;
                }
            }
            if (!delete && !edit)
            {
                listView.AllowSwiping = false;
            }
            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "add_category"))
            {
                xAdd.IsEnabled = true;
            }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
        }

        void SearchButtonPress(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (!string.IsNullOrWhiteSpace(searchBar.Text))
            {
                CategoryViewModel.RefreshCategory(searchBar.Text);
            }
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
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning.IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-UD1CV", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-UD1CV-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-UD1CV.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-UD1CV-" + err.ToString());
                }
                IsRunning.IsRunning = false;
            }
        }
    }
}