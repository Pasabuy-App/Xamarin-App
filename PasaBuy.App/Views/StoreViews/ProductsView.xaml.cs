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
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void AddTapped(object sender, EventArgs e)
        {
            //await Task.Delay(200);
            AddProductView.pdid = "0";
            //await AddIcon.FadeTo(0.3, 200);
            await Navigation.PushAsync(new AddProductView());
            //await AddIcon.FadeTo(1, 200);
            //var testPage = new NavigationPage(new AddProductView());
            //Navigation.PushAsync(testPage);
            /*if (AddProductButton.IsEnabled == true)
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
            }*/
        }

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            LastIndex = 11;
            isFirstLoad = false;
            Offset = 0;
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
            }
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    count = 1;
                    bool answer = await DisplayAlert("Delete Product", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var btn = sender as Grid;
                        TindaPress.Product.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                LastIndex = 11;
                                isFirstLoad = false;
                                Offset = 0;
                                ProductViewModel.productsList.Clear();
                                ProductViewModel.LoadData("");
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
                /*LastIndex = 11;
                isFirstLoad = false;
                Offset = 0;*/
                var btn = sender as Grid;
                //new Alert("ID for Update", btn.ClassId, "OK");
                AddProductView.pdid = btn.ClassId;
                await Navigation.PushAsync(new AddProductView());
                await Task.Delay(200);
                count = 0;
            }
        }
    }
}