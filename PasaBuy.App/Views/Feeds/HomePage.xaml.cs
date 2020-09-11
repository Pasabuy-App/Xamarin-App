﻿using PasaBuy.App.Models.Feeds;
using PasaBuy.App.ViewModels.Feeds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentView
    {
        public HomePage()
        {
            InitializeComponent();
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

        private void homeListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            //var item = e.ItemData as Post;
            /*var itemTypeObject = e.ItemData as Post;
            if (HomepageViewModel.homePostList.Last() == itemTypeObject && HomepageViewModel.homePostList.Count() != 12)
            {
                //HomepageViewModel.LoadMore(itemTypeObject.Last_ID);
                Debug.WriteLine("Last ID "+ itemTypeObject.Last_ID);
            }*/
        }
    }
}