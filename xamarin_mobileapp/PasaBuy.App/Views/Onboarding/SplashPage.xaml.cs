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

            //Animate log here.
            await SplashLogo.ScaleTo(1.0, 1000);
            await SplashLogo.ScaleTo(1.5, 1500, Easing.Linear);

            if (Preferences.ContainsKey("ReturnUser"))
            {
                if (Preferences.ContainsKey("UserToken"))
                {
                    Application.Current.MainPage = new MainPage();
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