using System;

namespace PasaBuy.App.Local
{
    /// <summary>
    /// Application global configuration variables.
    /// </summary>
    public class PSAConfig
    {
        /// <summary>
        /// Rest API base url using WP PLugins.
        /// </summary>
        /// 
        public static bool onProduction { get; } = false;

        /// <summary>
        /// Does the current RestURL used SSL.
        /// </summary>
        public static bool onSSL { get; } = false;

        /// <summary>
        /// Development url.
        /// </summary>
        public static string devBaseUrl { get; } = "10.12.91.198";

        /// <summary>
        /// WordPress media host url.
        /// </summary>
        public static string devMediaUrl { get; } = "localhost/wordpress";

        /// <summary>
        /// RestAPI Root url c/o WordPress.
        /// </summary>
        public static string baseRestUrl { get; } = "pasabuy.app";

        /// <summary>
        /// Realtime USN hostname
        /// </summary>
        public static string USocketNetHostname { get; } = "usn.pasabuy.app";

        /// <summary>
        /// Get the current active Rest URL dev or prod.
        /// </summary>
        public static string CurrentRestUrl
        {
            get
            {
                string returning = PSAConfig.onSSL ? "https://" : "http://";

                if (PSAConfig.onProduction)
                {
                    Console.WriteLine(returning + baseRestUrl);
                    return returning + baseRestUrl;
                }

                Console.WriteLine(returning + devBaseUrl);

                return returning + devBaseUrl;
            }
        }

        /// <summary>
        /// Syncfusion Demo image root URL.
        /// </summary>
        public static string sfRootUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/"; //For Demo only

        /// <summary>
        /// Syncfusion Application API Key Secret
        /// </summary>
        public static string sfApiKey { get; } = "MjYwNjY0QDMxMzgyZTMxMmUzMGlJTnpSZVBDTG4raGhBaU5DOVRFbFBKWmxRYVVHd1hxNUx2cXcrZFliMmc9"; //Test Key
        public const string googleApiKey = "AIzaSyDNDC5kv3pGvM-1zhPXU8yFewflraQaGDs"; //Test Key
        public const string GeoMatrixGoogleApiKey = "AIzaSyDeR29pTg1D5Exga3rQd8a3XKL3XtukQQg"; //Test Key

    }
}
