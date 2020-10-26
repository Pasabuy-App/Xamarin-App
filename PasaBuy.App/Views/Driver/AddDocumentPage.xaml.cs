using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Navigation;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDocumentPage : ContentPage
    {
        public string filepath = string.Empty;
        public AddDocumentPage()
        {
            InitializeComponent();
            List<string> list = new List<string>();
            if (MasterView.MyType == "store")
            {
                list.Add("DTI Registration");
                list.Add("Barangay Clearance");
                list.Add("Lease Contract");
                list.Add("Community Tax");
                list.Add("Occupancy Permit");
                list.Add("Sanitary Permit");
                list.Add("Fire Permit");
                list.Add("Mayor's Permit");
            }
            if (MasterView.MyType == "mover")
            {
                list.Add("License Card");
                list.Add("License OR");
                list.Add("Vehicle CR");
                list.Add("Vehicle OR");
                list.Add("Vehicle's Front");
                list.Add("Vehicle's Right");
                list.Add("Vehicle's Left");
                list.Add("Vehicle's Back");
            }
            DocumentTypePicker.DataSource = list;
        }

        void RemoveDocumentImage(object sender, EventArgs args)
        {
            DocumentImage.Source = "document.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                CompressionQuality = 30
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            DocumentImage.Source = imageSource;
            filepath = file.Path;
        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                SaveMetaData = false,
                CompressionQuality = 30,
                MaxWidthHeight = 1024
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            DocumentImage.Source = imageSource;
            filepath = file.Path;

        }
    }
}