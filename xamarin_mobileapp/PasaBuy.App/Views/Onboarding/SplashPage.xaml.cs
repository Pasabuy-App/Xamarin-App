using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            CheckConnectivity();
        }

        public static void CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                CheckToken();
            }

            else
            {
                Application.Current.MainPage = new NavigationPage(new PasaBuy.App.Views.ErrorAndEmpty.NoInternetConnectionPage());
            }
        }

        public static void CheckToken()
        {
            if ( UserPrefs.Instance.hasToken )
            {
                if ( UserPrefs.Instance.hasUserinfo )
                {
                    Application.Current.MainPage = new MainTabs();
                }

                else
                {
                    Application.Current.MainPage = new NavigationPage(new SignInPage());
                }
            }

            else
            {
                Application.Current.MainPage = new NavigationPage(new GettingStarted());
            }
        }
    }
}