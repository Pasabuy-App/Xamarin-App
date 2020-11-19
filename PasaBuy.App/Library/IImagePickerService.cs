using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PasaBuy.App.Library
{
    public interface IImagePickerService
    {
        /// <summary>
        /// Pick a image from camera or gallery.
        /// </summary>
        /// <returns>Picked image as ImageSource or null when user cancel.</returns>
        Task<ImageSource> PickImageAsync();

        /// <summary>
        /// ImageSource utility.
        /// </summary>
        IImageSourceUtility ImageSourceUtility { get; }
    }
}
