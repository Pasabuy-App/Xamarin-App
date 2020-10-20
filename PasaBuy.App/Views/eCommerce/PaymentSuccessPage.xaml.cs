using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    /// <summary>
    /// Page to show the payment success.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSuccessPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentSuccessPage" /> class.
        /// </summary>
        public PaymentSuccessPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
                for (int currModal = 0; currModal < numModals; currModal++)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync(false);
                }
            });
            return base.OnBackButtonPressed();
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
            for (int currModal = 0; currModal < numModals; currModal++)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync(false);
            }
        }
    }
}