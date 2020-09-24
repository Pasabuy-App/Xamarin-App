using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsView : ContentPage
    {
        public static int LastIndex = 11;
        public ICommand AddProductCommand { get; set; }

        public ProductsView()
        {
            InitializeComponent();
            AddProductButton.Clicked += AddProductClicked;
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        private async void AddProductClicked(object sender, EventArgs e)
        {
            //var testPage = new NavigationPage(new AddProductView());
            //Navigation.PushAsync(testPage);
            if (AddProductButton.IsEnabled == true)
            {
                AddProductButton.IsEnabled = false;
                AddProductCommand = new Command<Type>(async (Type pageType) =>
                {
                    Page page = (Page)Activator.CreateInstance(pageType);
                    await Navigation.PushAsync(page);
                });
                BindingContext = this;
                await Task.Delay(200);
                AddProductButton.IsEnabled = true;
            }
        }

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            ProductViewModel.productsList.Clear();
            ProductViewModel.LoadData("");
            pullToRefresh.IsRefreshing = false;
        }

        private void listView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as ProductData;
            if (ProductViewModel.productsList.Last() == item && ProductViewModel.productsList.Count() != 1)
            {
                if (ProductViewModel.productsList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    ProductViewModel.LoadData(item.ID);
                }
            }
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Product?", "Are you sure to delete this?", "Yes", "No");
            if (answer)
            {
                try
                {
                    var btn = sender as Grid;
                    TindaPress.Product.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                    {
                        if (success)
                        {
                            LastIndex = 11;
                            ProductViewModel.productsList.Clear();
                            ProductViewModel.LoadData("");
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20416.", "OK");
                }
            }
        }

        private void Update_Tapped(object sender, EventArgs e)
        {
            LastIndex = 11;
            var btn = sender as Grid;
            new Alert("ID for Update", btn.ClassId, "OK");
        }
    }
}