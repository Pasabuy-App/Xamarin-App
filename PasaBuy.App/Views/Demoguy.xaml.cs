using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.ImagePicker;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demoguy : ContentPage
    {
        IImagePickerService _imagePickerService;
        public Demoguy()
        {
            _imagePickerService = DependencyService.Get<IImagePickerService>();
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var imageSource = await _imagePickerService.PickImageAsync();

            var fileName = imageSource.ToString();

            new Alert("ok", fileName, "ok");

            if (imageSource != null) // it will be null when user cancel
            {
                Img.Source = imageSource;
            }
            
        }
    }
}