using Xamarin.Forms;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Settings;

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

            MainPage = new NavigationPage(new ChangePWPage());
        }

        protected override void OnStart()
        {
            //called when the application starts.
        }

        protected override void OnSleep()
        {
            //called each time the application goes to the background.
        }

        protected override void OnResume()
        {
            //called when the application is resumed, after being sent to the background.
        }
    }
}
