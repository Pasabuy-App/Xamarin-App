using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInPage" /> class.
        /// </summary>


        public SignInPage()
        {
            InitializeComponent();
            SignupButton.IsEnabled = true;
        }

        private async void SignUpClicked(object sender, System.EventArgs e)
        {
            SignupButton.IsEnabled = false;
            Loader.IsVisible = true;
            Loader.IsRunning = true;
            await Navigation.PushModalAsync(new SignUpPage());
            SignupButton.IsEnabled = true;
            Loader.IsVisible = false;
            Loader.IsRunning = false;
        }
    }
}