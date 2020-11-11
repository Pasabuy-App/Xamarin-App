using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demoguy : ContentPage
    {
        public Demoguy()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupRateDriver());

        }
    }
}