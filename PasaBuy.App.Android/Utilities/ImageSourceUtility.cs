using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using PasaBuy.App.Library;

namespace PasaBuy.App.Droid.Utilities
{
    public class ImageSourceUtility : IImageSourceUtility
    {
        public Task<Stream> ToJpegStreamAsync(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            if (imageSource is FileImageSource)
            {
                return Task.Run(() => File.Open((imageSource as FileImageSource).File, FileMode.Open) as Stream);
            }
            else
            {
                throw new InvalidOperationException($"The type of the given imageSource is '{imageSource.GetType().Name}', but 'FileImageSource' were expected.");
            }
        }
    }
}