using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PasaBuy.App.Library;
using UIKit;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace PasaBuy.App.iOS.Utilities
{
    public class ImageSourceUtility : IImageSourceUtility
    {
        public async Task<Stream> ToJpegStreamAsync(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            if (imageSource is StreamImageSource)
            {
                var imageStream = new StreamImagesourceHandler();
                var image = await imageStream.LoadImageAsync(imageSource);
                return image.AsJPEG().AsStream();
            }
            else
            {
                throw new InvalidOperationException($"The type of the given imageSource is '{imageSource.GetType().Name}', but 'StreamImageSource' were expected.");
            }
        }

    }
}