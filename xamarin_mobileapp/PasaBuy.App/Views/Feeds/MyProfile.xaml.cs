using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds
{
    /// <summary>
    /// Page to show the social profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyProfile" /> class.
        /// </summary>
        public MyProfile()
        {
            InitializeComponent();
        }
    }
}