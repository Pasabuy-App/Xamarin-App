using PasaBuy.App.Local;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.Models
{
    /// <summary>
    /// Model for SocialProfile
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Profile
    {
        #region Fields

        private string imagePath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        public string ImagePath
        {
            get { return PSAConfig.sfRootUrl + this.imagePath; }
            set { this.imagePath = value; }
        }

        #endregion
    }
}