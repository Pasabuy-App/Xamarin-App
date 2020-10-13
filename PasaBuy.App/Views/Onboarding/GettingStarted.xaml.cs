using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GettingStarted
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GettingStarted" /> class.
        /// </summary>
        public GettingStarted()
        {
            InitializeComponent();
        }
    }
}