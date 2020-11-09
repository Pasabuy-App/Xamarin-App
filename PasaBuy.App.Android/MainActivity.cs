using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Webkit;
using Plugin.CurrentActivity;
using Plugin.LocalNotification;
using Plugin.Permissions;
using Syncfusion.XForms.Android.PopupLayout;
using System;
using System.Linq;

namespace PasaBuy.App.Droid
{
    [Activity(Label = "PasaBuy.App", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static Context Context;

        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation
        };

        public String mGeolocationOrigin;
        public GeolocationPermissions.ICallback mGeolocationCallback;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Context = this.ApplicationContext;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            SfPopupLayoutRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            NotificationCenter.CreateNotificationChannel();
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());

            NotificationCenter.NotifyNotificationTapped(Intent);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum]Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (permissions.Contains(Manifest.Permission.AccessFineLocation))
            {
                bool allow = false;
                if (grantResults[0] == Android.Content.PM.Permission.Granted)
                {
                    // user has allowed these permissions
                    allow = true;
                }
                if (mGeolocationCallback != null)
                {
                    mGeolocationCallback.Invoke(mGeolocationOrigin, allow, false);
                }
            }
        }


        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else
                {
                    // Permissions already granted - display a message.
                }
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }
    }
}