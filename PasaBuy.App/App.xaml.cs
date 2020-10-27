
using PasaBuy.App.Local;
using PasaBuy.App.Local.Notice;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.Onboarding;
using PasaBuy.App.Views.PopupModals;
using System;
using Xamarin.Forms;

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
            CoinPress.CPHost.Instance.Initialized(PSAConfig.CurrentRestUrl);

            PSACache.Instance.Initialize();

            //commit
            MainPage = new NavigationPage(new SplashPage());

            LocalNotif.Instance.Show("Test Notification", "Elon Musk partnered with PasaBuy.App to deliver package to Mars.", "Its now done!", DateTime.Now.AddSeconds(60));
        }

        protected override void OnStart()
        {
            //AppCenter.Start("android=64cc939e-3ec6-4a16-955c-7ebab790b4e7;" +
             //     "uwp={Your UWP App secret here};" +
             //     "ios=9802b0e1-3a25-41a7-827b-cb7fd29b548b;",
             //    typeof(Analytics), typeof(Crashes));

            //Analytics.TrackEvent("AppStart");
        }

        protected override void OnSleep()
        {
            //Analytics.TrackEvent("AppSleep");
        }

        protected override void OnResume()
        {
            var nav = MainPage.Navigation;

            // you may want to clear the stack (history)
            //await nav.PopToRootAsync(true);

            // then open the needed page (I'm guessing a login page)
            //await nav.PushAsync(new LoginPage());

            //Analytics.TrackEvent("AppResume");
        }
    }
}
