using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Syncfusion.XForms.Android.PopupLayout;

namespace PasaBuy.App.Droid
{
    [Activity(Label = "PasaBuy.App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static Context Context;

        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation
        };

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
            Forms9Patch.Droid.Settings.Initialize(this);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            SfPopupLayoutRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            LoadApplication(new App());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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


    }
}