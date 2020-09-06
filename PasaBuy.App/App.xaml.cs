using PasaBuy.App.Controllers;
using PasaBuy.App.Local;
using PasaBuy.App.Views.ErrorAndEmpty;
using PasaBuy.App.Views.Notification;
using PasaBuy.App.Views.Onboarding;
using Xamarin.Essentials;
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
            DataVice.DVHost.Instance.Initialized(PSAConfig.baseRestUrl);
            SocioPress.SPHost.Instance.Initialized(PSAConfig.baseRestUrl);

            PSACache.Instance.Initialize();

            MainPage = new NavigationPage(new SplashPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
