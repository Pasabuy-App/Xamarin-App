using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Feeds;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SocioPress;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostStatusPage : ContentPage
    {
        private string filePath = string.Empty;
        public PostStatusPage()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        async void AddStatusImage(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "StatusImageFolder",
                SaveToAlbum = true,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            StatusImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;
        }

        //test commmit
        async void TakePhoto(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "item-image.jpg"
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            StatusImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;

        }

        async void SelectPhoto(object sender, EventArgs args)
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

            StatusImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;

        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            //Console.WriteLine("image filepath ." + filePath + ". " + StatusEditor.Text); //-> image file path upload
            /*SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "title123", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                    Navigation.PopModalAsync();
                    HomepageViewModel.RefreshList();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });*/
            /*SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "title123", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                    Navigation.PopModalAsync();
                    HomepageViewModel.RefreshList();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
            /*Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "title123", StatusEditor.Text, "status", (bool success, string data) =>
            {
                *//*if (success)
                {
                    ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                    Navigation.PopModalAsync();
                    HomepageViewModel.RefreshList();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }*//*
                Console.WriteLine("image filepath ." + filePath + ". " + StatusEditor.Text); //-> image file path upload
            });*/
            /*SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "title123", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                    Navigation.PopModalAsync();
                    HomepageViewModel.RefreshList();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });*/
        }
    }
}