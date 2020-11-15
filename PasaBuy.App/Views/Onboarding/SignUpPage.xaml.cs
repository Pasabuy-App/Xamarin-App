using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Onboarding;
using PasaBuy.App.Views.Settings;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SignUpPage" /> class.
        /// </summary>
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void backButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Terms_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new TermsPage());
        }

        private void Privacy_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new PrivacyPage());
        }
    }
}