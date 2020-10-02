using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using PasaBuy.App.Library;
using Xamarin.Android.ImageCropper;
using Xamarin.Essentials;
using Xamarin.Forms;
using Path = System.IO.Path;

[assembly: Dependency(typeof(PasaBuy.App.Droid.Utilities.ImagePickerService))]
namespace PasaBuy.App.Droid.Utilities
{

    public class ImagePickerService : IImagePickerService
    {
        public IImageSourceUtility ImageSourceUtility => new ImageSourceUtility();

        private void StartActivity()
        {
            var currentActivity = Forms.Context as Activity;

            if (currentActivity != null)
            {
                var cropImageOptions = new CropImageOptions();
                cropImageOptions.MultiTouchEnabled = true;
                cropImageOptions.Guidelines = CropImageView.Guidelines.On;
                cropImageOptions.AspectRatioX = 1;
                cropImageOptions.AspectRatioY = 1;
                cropImageOptions.FixAspectRatio = true;
                cropImageOptions.Validate();
                var intent = new Intent();
                intent.SetClass(currentActivity, Class.FromType(typeof(ImagePickerOnResultActivity)));
                intent.PutExtra(CropImage.CropImageExtraSource, null as global::Android.Net.Uri); // Image Uri
                intent.PutExtra(CropImage.CropImageExtraOptions, cropImageOptions);
                currentActivity.StartActivity(intent);
            }
            else
            {
                throw new InvalidOperationException("Could not get current activity.");
            }

        }

  
        public Task<ImageSource> PickImageAsync()
        {
            StartActivity();

            return Task.Run(() =>
            {
                _waitHandle.WaitOne();
                var result = _pickAsyncResult;
                _pickAsyncResult = null;

                return result;
            });
        }

        private static ImageSource _pickAsyncResult;
        private static EventWaitHandle _waitHandle = new AutoResetEvent(false);

        [Activity(Theme = "@style/Base.Theme.AppCompat")]
        public class ImagePickerOnResultActivity : CropImageActivity
        {
            public override void OnCropImageComplete(CropImageView cropImageView, CropImageView.CropResult cropResult)
            {
                var resultImageUri = new Uri(cropResult.Uri.ToString());
                _pickAsyncResult = ImageSource.FromFile(resultImageUri.LocalPath);
                base.OnCropImageComplete(cropImageView, cropResult);
                _waitHandle.Set();
            }

        


        }

    }
}