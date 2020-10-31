using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Chat;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailsView : ContentPage
    {
        public TransactionDetailsView()
        {
            InitializeComponent();
            this.BindingContext = new OrderDetailsViewModel();
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Declined_Clicked(object sender, EventArgs e)
        {

        }

        private void Accepted_Clicked(object sender, EventArgs e)
        {

        }

        private void MessageCustomer(object sender, EventArgs e)
        {

        }
    }
}