using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using PasaBuy.App.iOS;
using PasaBuy.App.Library;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GpsDependencyService))]
namespace PasaBuy.App.iOS
{
    public class GpsDependencyService : IGpsDependencyService
    {
        public bool IsGpsEnable()
        {
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void OpenSettings()
        {
            var WiFiURL = new NSUrl("prefs:root=WIFI");

            if (UIApplication.SharedApplication.CanOpenUrl(WiFiURL))
            {   //> Pre iOS 10
                UIApplication.SharedApplication.OpenUrl(WiFiURL);
            }
            else
            {   //> iOS 10
                UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
            }
        }
    }
}