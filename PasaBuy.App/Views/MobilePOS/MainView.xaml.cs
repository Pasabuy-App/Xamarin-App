using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Plugin.Vibrate;
using Plugin.Permissions;
using PasaBuy.App.Resources.Texts;

namespace PasaBuy.App.Views.MobilePOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            //MessagingCenter.Unsubscribe<string>(this, MiniPOS.AppSettings.Settings.ZXingRequestCameraFailed);
            //MessagingCenter.Subscribe<string>(this, MiniPOS.AppSettings.Settings.ZXingRequestCameraFailed, (arg) =>
            //{
            //    Navigation.PopToRootAsync();
            //});
        }

        [Obsolete]
        public async void ScanBarcode(object sender, EventArgs e)
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);
            
            //if (PermissionStatus.Granted)
            //{
            //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Camera);
            //    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Camera))
            //        cameraStatus = results[Plugin.Permissions.Abstractions.Permission.Camera];
            //}

            //if (cameraStatus == PermissionStatus.Granted)
                //ScanCustomOverlayAsync();
        
        }

        private async void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                //Context.ScannedText = e.NewTextValue;
                //await Context.SearchProductWhenScan();
            }
        }
    }
}