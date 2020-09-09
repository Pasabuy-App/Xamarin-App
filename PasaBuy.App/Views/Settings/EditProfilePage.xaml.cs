﻿using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataVice;
using Plugin.Media;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public string avatarUrl;
        public string bannerUrl;


        public EditProfilePage()
        {
            InitializeComponent();

            Fname.Text = PSACache.Instance.UserInfo.fname;
            Lname.Text = PSACache.Instance.UserInfo.lname;
            Nname.Text = PSACache.Instance.UserInfo.dname;
            Avatar.Source = PSACache.Instance.UserInfo.avatar;
            Banner.Source = PSACache.Instance.UserInfo.banner;

        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SaveAvatar(object sender, EventArgs e)
        {
            if (avatarUrl == null)
            {
                new Alert("Failed", "Please select an image for avatar first", "Ok");
                return;
            }
            Upload.Instance.Image(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, avatarUrl, "", "", "avatar", "", (bool success, string data) =>
            {
                if(success)
                {
                    new Alert("Success", "Avatar successfully updated", "Ok");
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }

        public void SaveBanner(object sender, EventArgs e)
        {
            if(bannerUrl == null)
            {
                new Alert("Failed", "Please select an image for banner first", "Ok");
                return;
            }
            Upload.Instance.Image(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, bannerUrl, "", "", "banner", "", (bool success, string data) =>
            {
                if (success)
                {
                    new Alert("Success", "Banner successfully updated", "Ok");
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }

        public void SaveProfileData(object sender, EventArgs e)
        {
            Users.Instance.EditProfile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, Fname.Text, Lname.Text, Nname.Text, (bool success, string data) =>
            {
                if (success)
                {
                    PSACache.Instance.UserInfo.dname = Nname.Text;
                    PSACache.Instance.UserInfo.lname = Lname.Text;
                    PSACache.Instance.UserInfo.fname = Fname.Text;
                    new Alert("Success", "Data successfully updated", "Ok");
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }



        async void AddAvatar(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            Avatar.Source = imageSource;
            avatarUrl = file.Path;
        }

        async void AddBanner(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            Banner.Source = imageSource;
            bannerUrl = file.Path;
        }
    }
}