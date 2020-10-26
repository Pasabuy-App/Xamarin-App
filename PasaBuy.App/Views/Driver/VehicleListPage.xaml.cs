using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleListPage : ContentPage
    {
        public VehicleListPage()
        {
            InitializeComponent();
            this.BindingContext = new VehicleListViewModel();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("tapped");
            Navigation.PopModalAsync();
        }

        private async void AddVehiclesTapped(object sender, EventArgs e)
        {
            await AddButton.FadeTo(0.5, 100);
            await AddButton.FadeTo(1, 100);
            await PopupNavigation.Instance.PushAsync(new PopupVehiclesSelection());
        }

        private void MyVehicle_Tapped(object sender, EventArgs e)
        {

        }
    }
}