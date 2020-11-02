using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VariantsView : ContentPage
    {
        public static int LastIndex = 11;
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;

        public VariantsView()
        {
            InitializeComponent();
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            LastIndex = 11;
            isFirstLoad = false;
            Offset = 0;
            ProductViewModel.productsList.Clear();
            ProductViewModel.RefreshProduct("");
            pullToRefresh.IsRefreshing = false;
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
    }
}