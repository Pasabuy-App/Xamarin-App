using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreListPage : ContentPage
    {
        public static int LastIndex = 11;
        public static string catid = string.Empty;
        public StoreListPage()
        {
            InitializeComponent();
        }

        private void StoreTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as Store;
            //App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
            //StoreDetailsViewModel.loadcategory(item.Id);
            //StoreDetailsViewModel.loadstoredetails(item.Id);

            StoreDetailsViewModel.store_id = item.Id;
            StoreDetailsViewModel.loadcategory(item.Id);
            StoreDetailsViewModel.loadstoredetails(item.Id);
            App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ManagementItems_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Store;
            if (StoreBrowserViewModel.storelist.Last() == item && StoreBrowserViewModel.storelist.Count() != 1)
            {
                if (StoreBrowserViewModel.storelist.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    StoreBrowserViewModel.LoadStore(catid, item.Id);
                }
            }
        }
    }
}