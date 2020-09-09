using PasaBuy.App.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Views.ErrorAndEmpty;
using PasaBuy.App.Local;
using PasaBuy.App.Library;

namespace PasaBuy.App.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SplashLogo.ScaleTo(1.0, 1000);
            await SplashLogo.ScaleTo(1.5, 1500, Easing.Linear);

            CheckConnectivityAndToken();
        }

        public static void CheckConnectivityAndToken()
        {
            if (PSADevice.HasInternet )
            {
                if (PSACache.Instance.hasUserinfo)
                {
                    App.Current.MainPage = new MainTabs();

                    PSAUsocketNet usn = new PSAUsocketNet();
                    usn.Connect();

                    return; //Cancel all after this line.
                }

                App.Current.MainPage = new SignInPage();

                if ( !PSACache.DoneWithGettingStarted )
                {
                    App.Current.MainPage = new GettingStarted();
                }
            }

            else
            {
                App.Current.MainPage = new NoInternetPage();
            }
        }

    }
}