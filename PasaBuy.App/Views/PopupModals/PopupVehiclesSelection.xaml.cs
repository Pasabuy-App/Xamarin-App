using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupVehiclesSelection : PopupPage
    {
        public PopupVehiclesSelection()
        {
            InitializeComponent();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void BicycleTapped(object sender, EventArgs e)
        {
            await BicycleGrid.FadeTo(0.5, 100);
            await BicycleGrid.FadeTo(1, 100);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new PasaBuy.App.Views.StoreViews.DocumentsView());

        }

        private async void MotorTapped(object sender, EventArgs e)
        {
            await MotorGrid.FadeTo(0.5, 100);
            await MotorGrid.FadeTo(1, 100);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new DriverDocuments());

        }

        private async void WheeledTapped(object sender, EventArgs e)
        {
            await WheeledGrid.FadeTo(0.5, 100);
            await WheeledGrid.FadeTo(1, 100);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new DriverDocuments());

        }

        private async void VanTapped(object sender, EventArgs e)
        {
            await VanGrid.FadeTo(0.5, 100);
            await VanGrid.FadeTo(1, 100);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new DriverDocuments());

        }
    }
}