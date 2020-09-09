using PasaBuy.App.Controllers.Notice;
using Plugin.Media;
using Android.Runtime;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostSellPage : ContentPage
    {
        private string filePath = string.Empty;
        public PostSellPage()
        {
            InitializeComponent();

        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            try
            {
                SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ItemName.Text, ItemDescription.Text, "sell", filePath, ItemCategory.Text, ItemPrice.Text, PickUpLocation.Text, "", VehicleType.Text, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                        Navigation.PopModalAsync();
                        HomepageViewModel.RefreshList();
                        MyProfileViewModel.RefreshList();
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        


        async void AddItemImage(object sender, EventArgs args)
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

            ItemImage.Source = imageSource;
            var filePath = file.Path;
            //filePath = file.Path;

        }

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

            ItemImage.Source = imageSource;
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

            ItemImage.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;
        }




    }
}