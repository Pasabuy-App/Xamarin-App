﻿using PasaBuy.App.Models.Feeds;
using PasaBuy.App.ViewModels.Feeds;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        public static int LastIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyProfile" /> class.
        /// </summary>
        public MyProfile()
        {
            InitializeComponent();
            LastIndex = 12;
            listView.Scrolled += OnCollectionViewScrolled;
        }
        async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex > LastIndex)
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    MyProfileViewModel.LoadMore(LastIndex.ToString());
                    LastIndex += 7;
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
            }
        }

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton = string.Empty;
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

       /* private void profileListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Post;
            if (MyProfileViewModel.profilePostList.Last() == item && MyProfileViewModel.profilePostList.Count() != 1)
            {
                if (MyProfileViewModel.profilePostList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    MyProfileViewModel.LoadMore(item.Last_ID);
                }
            }
        }*/
    }
}