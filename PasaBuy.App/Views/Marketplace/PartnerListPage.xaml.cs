using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerListPage : ContentPage
    {
        public static int LastIndex = 11;
        public static string catid = string.Empty;
        public static string pageTitle;
        public bool isTapped;
        public PartnerListPage()
        {
            InitializeComponent();
            this.BindingContext = new PartnerListViewModel();

            PageTitle.Text = pageTitle;
            isTapped = false;
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void StoreList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Store;
            if (PartnerListViewModel.storeList.Last() == item && PartnerListViewModel.storeList.Count() != 1)
            {
                if (PartnerListViewModel.storeList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    PartnerListViewModel.LoadMore(catid, item.Id);
                }
            }
        }

        private async void StoreList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Store;
                //App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                //StoreDetailsViewModel.loadcategory(item.Id);
                //StoreDetailsViewModel.loadstoredetails(item.Id);

                StoreDetailsViewModel.store_id = item.Id;
                //StoreDetailsViewModel.Loadcategory(item.Id);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                isTapped = false;
            }
        }
    }
}