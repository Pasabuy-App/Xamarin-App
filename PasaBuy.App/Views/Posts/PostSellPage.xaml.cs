﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.ImagePicker;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostSellPage : ContentPage
    {
        IImagePickerService _imagePickerService;
        private Boolean btn = false;
        private string filePath = string.Empty;
        public PostSellPage()
        {
            _imagePickerService = DependencyService.Get<IImagePickerService>();
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
                Loader.IsRunning = true;
                Loader.IsVisible = true;

                if (!string.IsNullOrWhiteSpace(ItemName.Text) || !string.IsNullOrWhiteSpace(ItemDescription.Text) || !string.IsNullOrWhiteSpace(PickUpLocation.Text) || !string.IsNullOrWhiteSpace(ItemPrice.Text) || !string.IsNullOrWhiteSpace(VehicleType.Text) || !string.IsNullOrWhiteSpace(ItemCategory.Text))
                {
                    if (btn == false)
                    {
                        btn = true;
                        Http.SocioPress.Post.Instance.Insert(ItemName.Text, ItemDescription.Text, "sell", filePath, ItemCategory.Text, ItemPrice.Text, PickUpLocation.Text, "", VehicleType.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                                {
                                    Views.Feeds.HomePage.LastIndex = 12;
                                    Views.Feeds.HomePage.isFirstLoad = false;
                                    HomepageViewModel.homePostList.Clear();
                                    HomepageViewModel.LoadData("");
                                }
                                else
                                {
                                    MyProfileViewModel.profilePostList.Clear();
                                    MyProfileViewModel.LoadData(PSACache.Instance.UserInfo.wpid);
                                }
                                Navigation.PopModalAsync();
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                            }
                        });
                    }
                }
                else
                {
                    new Alert("Notice to user", "Required fields cannot be empty.", "OK");
                    Loader.IsRunning = false;
                    Loader.IsVisible = false;
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: SPV1PST-I1PSLP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1PST-I1PSLP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-I1PSLP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1PST-I1PSLP-" + err.ToString());
                }
            }
            Loader.IsRunning = false;
            Loader.IsVisible = false;
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
            var imageSource = await _imagePickerService.PickImageAsync();
            await Task.Delay(500);
            char[] charsToTrim = { ':', ' ' };
            var fileName = imageSource.ToString().Remove(0, 4);

            if (imageSource != null) // it will be null when user cancel
            {
                ItemImage.Source = imageSource;
            }
            filePath = fileName.Trim(charsToTrim);
            await Task.Delay(500);
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