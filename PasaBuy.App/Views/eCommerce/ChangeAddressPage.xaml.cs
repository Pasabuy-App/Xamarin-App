using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeAddressPage : ContentPage
    {
        public ChangeAddressPage()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}