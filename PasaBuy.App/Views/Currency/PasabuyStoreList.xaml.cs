using PasaBuy.App.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasabuyStoreList : ContentPage
    {
        public PasabuyStoreList()
        {
            InitializeComponent();
            this.BindingContext = new PasabuyStoreListViewModel();
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}