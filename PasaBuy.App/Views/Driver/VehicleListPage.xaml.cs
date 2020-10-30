using Forms9Patch;
using PasaBuy.App.Local;
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
        public int count = 0;
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
            Loader.IsRunning = true;
            await System.Threading.Tasks.Task.Delay(500);
            if (count == 0)
            {
                count = 1;
                await PopupNavigation.Instance.PushAsync(new PopupVehicleDetails());
                await System.Threading.Tasks.Task.Delay(200);
                count = 0;
            }
            Loader.IsRunning = false;
        }
    }
}