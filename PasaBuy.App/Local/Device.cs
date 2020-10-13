using Xamarin.Essentials;

namespace PasaBuy.App.Local
{
    /// <summary>
    /// Check status all about the current device
    /// </summary>
    public class PSADevice
    {
        /// <summary>
        /// Check if the device is able to reach the internet.
        /// </summary>
        public static bool HasInternet
        {
            get
            {
                return Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
            }
        }
    }
}
