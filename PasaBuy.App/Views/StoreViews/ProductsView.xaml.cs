using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Linq;
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
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;
        public ICommand AddProductCommand { get; set; }

        public ProductsView()
        {
            InitializeComponent();
            this.BindingContext = new ProductViewModel();
            SearchText.SearchButtonPressed += SearchButtonPress;
        }

        void SearchButtonPress(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (!string.IsNullOrWhiteSpace(searchBar.Text))
            {
                ProductViewModel.RefreshProduct(searchBar.Text);
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void listView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            /*var item = e.ItemData as Models.TindaFeature.ProductModel;
            if (ProductViewModel.productsList.Last() == item && ProductViewModel.productsList.Count() != 1)
            {
                if (ProductViewModel.productsList.IndexOf(item) >= LastIndex)
                {
                    if (isFirstLoad)
                    {
                        Offset += 7;
                    }
                    else
                    {
                        isFirstLoad = true;
                    }
                    LastIndex += 6;
                    ProductViewModel.LoadData(Offset.ToString());
                }
            }*/
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Delete Product", "Are you sure to delete this?", "Yes", "No");
                if (answer)
                {
                    var btn = sender as Grid;
                    if (!IsRunning.IsRunning)
                    {
                        IsRunning.IsRunning = true;
                        Http.TindaPress.Product.Instance.Delete(btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                LastIndex = 11;
                                isFirstLoad = false;
                                Offset = 0;
                                ProductViewModel.RefreshProduct("");
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-D1", "OK");
                IsRunning.IsRunning = false;
            }
        }
    }
}