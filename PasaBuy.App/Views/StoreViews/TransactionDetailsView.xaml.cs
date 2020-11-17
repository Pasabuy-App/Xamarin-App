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
            ViewModels.MobilePOS.DashboardOrdersViewModel._starttimer = true;
            Navigation.PopModalAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            ViewModels.MobilePOS.DashboardOrdersViewModel._starttimer = true;
            return base.OnBackButtonPressed();
        }

    }
}