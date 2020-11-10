using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
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
            Navigation.PopModalAsync();
        }

        private async void AddVehiclesTapped(object sender, EventArgs e)
        {
            if (Loader.IsRunning == false)
            {
                Loader.IsRunning = true;
                await PopupNavigation.Instance.PushAsync(new PopupVehicleDetails());
                Loader.IsRunning = false;
            }
        }
    }
}