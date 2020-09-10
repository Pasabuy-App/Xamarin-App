using System;
using System.Collections.Generic;
using System.Text;

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
        /// Development url.
        /// </summary>
        public static string devBaseUrl { get; } = "localhost/wordpress";

        /// <summary>
        /// RestAPI Root url c/o WordPress.
        /// </summary>
        public static string baseRestUrl { get; } = "http://10.0.2.2/wordpress";

        /// <summary>
        /// Syncfusion Demo image root URL.
        /// </summary>
        public static string sfRootUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/"; //For Demo only

        /// <summary>
        /// Syncfusion Application API Key Secret
        /// </summary>
        public static string sfApiKey { get; } = "MjYwNjY0QDMxMzgyZTMxMmUzMGlJTnpSZVBDTG4raGhBaU5DOVRFbFBKWmxRYVVHd1hxNUx2cXcrZFliMmc9"; //Test Key
    }
}
