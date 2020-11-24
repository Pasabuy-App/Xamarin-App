using PasaBuy.App.Controllers.Notice;
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
    public partial class PostStatusPage : ContentPage
    {
        private Boolean btn = false;
        private string filePath = string.Empty;
        IImagePickerService _imagePickerService;

        public PostStatusPage()
        {
            _imagePickerService = DependencyService.Get<IImagePickerService>();
            InitializeComponent();
            
            //if (StatusImage.IsLoading)
            //{

            //}
            //StatusImage.Source = "https://i2.wp.com/seds.org/wp-content/uploads/2020/06/placeholder.png?fit=1200%2C800&ssl=1";
            //StatusImage.LoadingPlaceholder = "https://i2.wp.com/seds.org/wp-content/uploads/2020/06/placeholder.png?fit=1200%2C800&ssl=1";


        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        async void AddStatusImage(object sender, EventArgs args)
        {
            var imageSource = await _imagePickerService.PickImageAsync();
            await Task.Delay(500);
            char[] charsToTrim = { ':', ' ' };
            var fileName = imageSource.ToString().Remove(0, 4);

            if (imageSource != null) // it will be null when user cancel
            {
                StatusImage.Source = imageSource;
            }
            filePath = fileName.Trim(charsToTrim);
            await Task.Delay(500);
        }

        async void TakePhoto(object sender, EventArgs args)
        {
            var imageSource = await _imagePickerService.PickImageAsync();
            await Task.Delay(500);
            char[] charsToTrim = { ':', ' ' };
            var fileName = imageSource.ToString().Remove(0,4);

            if (imageSource != null) // it will be null when user cancel
            {
                StatusImage.Source = imageSource;
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

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(StatusEditor.Text))
                {
                    if (!Loader.IsRunning)
                    {
                        Loader.IsRunning = true;
                        Http.SocioPress.Post.Instance.Insert(StatusEditor.Text, "", "status", filePath, "", "", "", "", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                                {
                                    Views.Feeds.HomePage.LastIndex = 12;
                                    Views.Feeds.HomePage.isFirstLoad = false;
                                    HomepageViewModel.homePostList.Clear();
                                    HomepageViewModel.LoadData("");
                                    Loader.IsRunning = false;
                                }
                                else
                                {
                                    MyProfileViewModel.profilePostList.Clear();
                                    MyProfileViewModel.LoadData(PSACache.Instance.UserInfo.wpid);
                                }
                                Navigation.PopModalAsync();
                                Loader.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                Loader.IsRunning = false;
                            }
                        });
                    }
                }
                else
                {
                    new Alert("Notice to user", "Required fields cannot be empty.", "OK");
                    Loader.IsRunning = false;
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: SPV1PST-I1PSTP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1PST-I1PSTP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-I1PSTP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1PST-I1PSTP-" + err.ToString());
                }
                Loader.IsRunning = false;
            }
        }
    }
}