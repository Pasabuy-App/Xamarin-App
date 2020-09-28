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
using FFImageLoading;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostStatusPage : ContentPage
    {
        private Boolean btn = false;
        private string filePath = string.Empty;
        public PostStatusPage()
        {
            InitializeComponent();
            //if (StatusImage.IsLoading)
            //{

            //}
            StatusImage.Source = "https://i2.wp.com/seds.org/wp-content/uploads/2020/06/placeholder.png?fit=1200%2C800&ssl=1";
            StatusImage.LoadingPlaceholder = "https://i2.wp.com/seds.org/wp-content/uploads/2020/06/placeholder.png?fit=1200%2C800&ssl=1";
            

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
                CompressionQuality = 40
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
                Name = "item-image.jpg",
                CompressionQuality = 40,
                AllowCropping = true
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
                CompressionQuality = 40,

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
            try
            {
                if (!string.IsNullOrWhiteSpace(StatusEditor.Text))
                {
                    if (btn == false)
                    {
                        btn = true;
                        SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, StatusEditor.Text, "", "status", filePath, "", "", "", "", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                                {
                                    HomepageViewModel.homePostList.Clear();
                                    HomepageViewModel.LoadData("");
                                }
                                else
                                {
                                    MyProfileViewModel.profilePostList.Clear();
                                    MyProfileViewModel.LoadData(PSACache.Instance.UserInfo.wpid);
                                }
                                Navigation.PopModalAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
                else
                {
                    new Alert("Notice to user", "Required fields cannot be empty.", "OK");
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}