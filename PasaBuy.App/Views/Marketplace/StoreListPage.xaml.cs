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

            //StoreDetailsViewModel.store_id = item.Id;
            //StoreDetailsViewModel.loadcategory(item.Id);
            //StoreDetailsViewModel.loadstoredetails(item.Id);
            App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}