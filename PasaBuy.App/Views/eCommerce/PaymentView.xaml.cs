using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    /// <summary>
    /// The Payment view. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentView
    {
        public static string method = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentView" /> class.
        /// </summary>
        public PaymentView()
        {
            InitializeComponent();
        }

        private void Method_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                method = (sender as Syncfusion.XForms.Buttons.SfRadioButton).Text;
                if (method == "Cash on Delivery")
                {
                    method = "Cash";
                }
                if (method == "Savings Wallet")
                {
                    method = "Wallet";
                }
                /*if (method == "Card Payment")
                {
                    method = "Card";
                }*/
            }

        }
    }
}