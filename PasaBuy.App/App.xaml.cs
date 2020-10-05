using Xamarin.Forms;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Onboarding;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PasaBuy.App.Views.Marketplace;

namespace PasaBuy.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(PSAConfig.sfApiKey);

            //Initialized all PCL required by PasaBuy.App
            DataVice.DVHost.Instance.Initialized(PSAConfig.CurrentRestUrl);
            SocioPress.SPHost.Instance.Initialized(PSAConfig.CurrentRestUrl);
            TindaPress.TPHost.Instance.Initialized(PSAConfig.CurrentRestUrl);
            HatidPress.HPHost.Instance.Initialized(PSAConfig.CurrentRestUrl);
            MobilePOS.MPHost.Instance.Initialized(PSAConfig.CurrentRestUrl);
            PSACache.Instance.Initialize();

            //commit
            MainPage = new NavigationPage(new SplashPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=64cc939e-3ec6-4a16-955c-7ebab790b4e7;" +
                  "uwp={Your UWP App secret here};" +
                  "ios=9802b0e1-3a25-41a7-827b-cb7fd29b548b;",
                  typeof(Analytics), typeof(Crashes));

            Analytics.TrackEvent("AppStart");
        }

        protected override void OnSleep()
        {
            Analytics.TrackEvent("AppSleep");
        }

        protected override void OnResume()
        {
            Analytics.TrackEvent("AppResume");
        }
    }
}
