using PasaBuy.App.Views.Forms;
using PasaBuy.App.Views.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Splash
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

            //Check if currently login. TO DO!

            //Animate log here.
            await SplashLogo.ScaleTo(1.0, 1000);
            await SplashLogo.ScaleTo(1.5, 1500, Easing.Linear);

            //IF LOGIN
            //Application.Current.MainPage = new NavigationPage(new MasterDetailPage1());

            //IF NOT
            //Application.Current.MainPage = new NavigationPage(new LoginWithSocialIconPage());

            Application.Current.MainPage = new NavigationPage(new OnBoardingAnimationPage());
        }
    }
}