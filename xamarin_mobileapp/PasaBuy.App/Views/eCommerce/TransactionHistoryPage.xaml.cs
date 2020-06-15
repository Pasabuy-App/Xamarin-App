using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    /// <summary>
    /// Page to show the transaction history.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionHistoryPage : ContentPage
    {
        public TransactionHistoryPage()
        {
            InitializeComponent();
        }
    }
}