using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to retrieve the password forgotten.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPwPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPwPage" /> class.
        /// </summary>
        public ForgotPwPage()
        {
            InitializeComponent();
        }

        private void backButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}