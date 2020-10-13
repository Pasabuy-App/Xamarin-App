using PasaBuy.App.Views.Posts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupPasabuy : PopupPage
    {
        public bool isTapped;
        public PopupPasabuy()
        {
            InitializeComponent();

            this.pasabayInfo.Text = "A mover posts items or his whereabouts and asks the community if someone wants to have the items bought by him. The mover will then deliver the items.";
            this.pabiliInfo.Text = "A buyer requests a mover to buy items for him in a restaurant or store. The mover delivers the item right after the purchase.";
            this.pahatidInfo.Text = "A mover drops off a passenger at a destination.";
            this.pasakayInfo.Text = "A mover invites a passenger to share a ride towards a destination.";
            isTapped = false;

        }

        private async void GoToPasabuy(object sender, EventArgs e)
        {

            if (!isTapped)
            {
                isTapped = true;
                PopupNavigation.Instance.PopAsync();
                await Navigation.PushModalAsync(new PasabayPage());
                isTapped = false;
            }
        }
        private async void GoToPabili(object sender, EventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                PopupNavigation.Instance.PopAsync();
                await Navigation.PushModalAsync(new PabiliPage());
                isTapped = false;
            }
        }

        private async void GoToPahatid(object sender, EventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                PopupNavigation.Instance.PopAsync();
                await Navigation.PushModalAsync(new PahatidPage());
                isTapped = false;
            }
        }
    }
}