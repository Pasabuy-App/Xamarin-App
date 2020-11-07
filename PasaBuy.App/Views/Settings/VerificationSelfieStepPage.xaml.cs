using PasaBuy.App.Controllers.Notice;
using Plugin.Media;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationSelfieStepPage : ContentPage
    {
        private string filePath = string.Empty;
        private bool isEnable = false;
        public VerificationSelfieStepPage()
        {
            InitializeComponent();
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
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                CompressionQuality = 40
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            ImageId.Source = imageSource;
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

            ImageId.Source = imageSource;
            //var filePath = file.Path;
            filePath = file.Path;

        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                new Alert("Failed", "You haven't completed this step yet", "Ok");
            }
            else
            {
                if (!isEnable)
                {
                    VerificationFillPage.selfiePath = filePath;
                    Navigation.PushModalAsync(new VerificationFillPage());
                    isEnable = true;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(1000);
                        isEnable = false;
                    });
                }
            }
        }

    }
}