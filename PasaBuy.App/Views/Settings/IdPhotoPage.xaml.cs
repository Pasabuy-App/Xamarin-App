using PasaBuy.App.Controllers.Notice;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdPhotoPage : ContentPage
    {
        private string filePath = string.Empty;
        private bool isEnable = false;
        public IdPhotoPage()
        {
            InitializeComponent();
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        async void TakePhoto(object sender, EventArgs args)
        {
            GC.Collect();
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
               new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
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
            filePath = file.Path;

        }

        //async void SelectPhoto(object sender, EventArgs args)
        //{
        //    await CrossMedia.Current.Initialize();
        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        new Alert("Error", "No camera available", "Failed");
        //    }

        //    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
        //    {
        //        CompressionQuality = 40,

        //    });


        //    if (file == null)
        //        return;

        //    ImageSource imageSource = ImageSource.FromStream(() =>
        //    {
        //        var stream = file.GetStream();
        //        return stream;
        //    });

        //    ImageId.Source = imageSource;
        //    //var filePath = file.Path;
        //    filePath = file.Path;

        //}

        private void NextButtonClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filePath) || String.IsNullOrEmpty(IDNumberEntry.Text))
            {
                new Alert("Failed", "You haven't completed this step yet", "Ok");
            }
            else
            {
                if (!isEnable)
                {
                    VerificationFillPage.idPath = filePath;
                    VerificationFillPage.idnumber = IDNumberEntry.Text;
                    Navigation.PushModalAsync(new VerificationSelfieStepPage());
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