using PasaBuy.App.ViewModels.Currency;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasabuyPlusPage : ContentPage
    {
        public PasabuyPlusPage()
        {
            InitializeComponent();
            this.BindingContext = new PasabuyPlusViewModel();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ViewParticipatingStores(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PasabuyStoreList());
        }
    }
}