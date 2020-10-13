
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PasaBuy.App.Library
{
    public interface IImageSourceUtility
    {
        /// <summary>
        /// Convert an ImageSource to a jpeg stream.
        /// </summary>
        /// <param name="imageSource">ImageSource to be converted.</param>
        /// <returns>Stream for jpeg image.</returns>
        Task<Stream> ToJpegStreamAsync(ImageSource imageSource);
    }
}
