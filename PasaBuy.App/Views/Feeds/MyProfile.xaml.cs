using PasaBuy.App.ViewModels.Feeds;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

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

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton = string.Empty;
            HomepageViewModel.RefreshList();
            Navigation.PopModalAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await ShareUri("http://localhost/wordpress/status/15/");
        }
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }
    }
}