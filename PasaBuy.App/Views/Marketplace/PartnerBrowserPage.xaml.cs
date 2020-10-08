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
    public partial class PartnerBrowserPage : ContentView
    {
        public static bool isTapped = false;
        public PartnerBrowserPage()
        {
            InitializeComponent();
            this.BindingContext = new PartnerBrowserViewModel();
            PartnerBrowserViewModel.LoadCategory();
            Loading.IsRunning = false;
            Loading.IsVisible = false;
        }

        private async void Partner_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Loading.IsRunning = true;
            Loading.IsVisible = true;
            var item = e.ItemData as Categories;
            StoreBrowserViewModel.LoadStore(item.Id, "");
            await App.Current.MainPage.Navigation.PushModalAsync(new PartnerListPage());
            Loading.IsRunning = false;
            Loading.IsVisible = false;

        }
    }
}