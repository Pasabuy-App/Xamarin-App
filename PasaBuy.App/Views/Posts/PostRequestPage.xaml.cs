using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.CustomRenderers;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{   
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostRequestPage : ContentPage
    {
        private Boolean btn = false;
        private string filePath = string.Empty;
        public PostRequestPage()
        {
            InitializeComponent();
           
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            if (btn == false)
            {
                btn = true;
                try
                {
                    SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ItemName.Text, ItemDescription.Text, "move", filePath, "", "", PickUpLocation.Text, DropOffLocation.Text, VehicleType.Text, (bool success, string data) =>
                    {
                        if (success)
                        {
                            if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                            {
                                HomepageViewModel.homePostList.Clear();
                                HomepageViewModel.LoadData();
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
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator.", "OK");
                }
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