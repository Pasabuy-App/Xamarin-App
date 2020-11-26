using PasaBuy.App.Controllers.Notice;
using Plugin.Media;
using System;
using System.Collections.Generic;
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
            Instructions.Text = GetInstructions();
        }

        private string GetInstructions()
        {
            var random = new Random();
            var list = new List<string> { "• Make a selfie with peace hand sign.", "• Make a selfie with thumbs up hand sign.", "• Hold your chin while doing a selfie." , "• Hold your cheeks while doing a selfie.", "• Hold your right ears while doing a selfie.", "• Point your index finger upwards while doing a selfie" };
            int index = random.Next(list.Count);
            return list[index];

        }

        async void TakePhoto(object sender, EventArgs args)
        {
            await Task.Delay(500);
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            await Task.Delay(500);
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
                    VerificationFillPage.instructions = Instructions.Text;
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