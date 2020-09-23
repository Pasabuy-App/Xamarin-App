using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Resources.Texts;
using PasaBuy.App.ViewModels.MobilePOS.Base;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AddProductViewModel : ViewModelBase
    {
        public ICommand OpenCameraCommand => new Command(async () => await OpenCameraAsync());

        private ImageSource _productImageSource = ImageSource.FromStream(() =>
        {
            var stream = new MemoryStream();
            return stream;
        });

        public ImageSource ProductImageSource
        {
            get
            {
                return _productImageSource;
            }
            set
            {
                _productImageSource = value;
                RaisePropertyChanged(() => ProductImageSource);
            }
        }
        private async Task OpenCameraAsync()
        {
            new Alert("Test", "test", "ok");
            //var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);
            //if (cameraStatus != PermissionStatus.Granted)
            //{
            //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Camera);
            //    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Camera))
            //        cameraStatus = results[Plugin.Permissions.Abstractions.Permission.Camera];
            //}
            //var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
            //if (storageStatus != PermissionStatus.Granted)
            //{
            //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage);
            //    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Storage))
            //        storageStatus = results[Plugin.Permissions.Abstractions.Permission.Storage];
            //}

            //if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            //{
            //    await CrossMedia.Current.Initialize();
            //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //    {
            //        await DialogService.ShowAlertAsync(TextsTranslateManager.Translate("CamNotReady"), TextsTranslateManager.Translate("Warning"), "OK");
            //        return;
            //    }
            //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //    {
            //        //SaveToAlbum = true,
            //        //Directory = DateTime.Now.Date.ToShortDateString(),
            //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
            //        CompressionQuality = 30
            //        //Name = $"{DateTime.Now.Date.ToShortDateString()}.jpg"
            //    });

            //    if (file == null)
            //        return;
            //    ProductImageSource = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        return stream;
            //    });
            //}
        }
    }
}
