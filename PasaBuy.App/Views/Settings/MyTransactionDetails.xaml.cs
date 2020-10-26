using PasaBuy.App.ViewModels.Settings;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTransactionDetails : ContentPage
    {
        public MyTransactionDetails()
        {
            InitializeComponent();
            this.BindingContext = new MyTransactionDetailsViewModel();
        }

        private void backButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}