using PasaBuy.App.Controllers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Views.ErrorAndEmpty;

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

            DataVice.DVHost.Instance.Initialized("http://10.0.2.2/wordpress");
            SocioPress.SPHost.Instance.Initialized("http://10.0.2.2/wordpress");

            CheckConnectivityAndToken();
        }

        public static void CheckConnectivityAndToken()
        {
            if ( App.HasInternet )
            {
                if (UserPrefs.Instance.hasToken)
                {
                    if (UserPrefs.Instance.hasUserinfo)
                    {
                        App.Current.MainPage = new MainTabs();

                        return; //Cancel all after this line.
                    }
                }

                App.Current.MainPage = new SignInPage();

                if ( !App.DoneWithGettingStarted )
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