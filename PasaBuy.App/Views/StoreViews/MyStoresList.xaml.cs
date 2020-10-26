using PasaBuy.App.ViewModels.MobilePOS;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyStoresList : ContentPage
    {
        public MyStoresList()
        {
            InitializeComponent();
            this.BindingContext = new MyStoreListViewModel();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /*private async void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            MasterView.MyType = "store";
            var item = e.ItemData as Models.Marketplace.Store;
            PSACache.Instance.UserInfo.stid = item.Id;
            PSACache.Instance.UserInfo.store_name = item.Title;
            //PSACache.Instance.UserInfo.roid = uinfo.data.roid;
            PSACache.Instance.UserInfo.store_logo = item.Logo;
            PSACache.Instance.UserInfo.store_banner = item.Banner;

            //PSACache.Instance.SaveUserData();
            await Task.Delay(500);
            Device.BeginInvokeOnMainThread(async () =>
            {
                App.Current.MainPage = new NavigationView();
            });
           

        }*/
    }
}