using PasaBuy.App.ViewModels.Feeds;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using PasaBuy.App.Models.Feeds;
using System.Diagnostics;
using System.Linq;

namespace PasaBuy.App.Views.Feeds
{
    /// <summary>
    /// Page to show the social profile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfile : ContentPage
    {
        public static int LastIndex = 11;

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
            var button = (Button)sender;
            var classId = button.ClassId;
            await ShareUri(classId);
        }
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri
            });
        }

        private void profileListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Post;
            if (MyProfileViewModel.profilePostList.Last() == item && MyProfileViewModel.profilePostList.Count() != 1)
            {
                if (MyProfileViewModel.profilePostList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    Debug.WriteLine("Last ID " + item.Last_ID + " " + MyProfileViewModel.profilePostList.IndexOf(item));
                    MyProfileViewModel.LoadMore(item.Last_ID);
                }
            }
        }
    }
}