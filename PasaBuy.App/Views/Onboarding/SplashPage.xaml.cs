using Microsoft.AppCenter.Crashes;
using PasaBuy.App.Local;
using PasaBuy.App.Views.ErrorAndEmpty;
using System;
using USocketNet;
using USocketNet.Model;
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

            /*await SplashLogo.ScaleTo(1.0, 1000);
            await SplashLogo.ScaleTo(1.5, 1500, Easing.Linear);*/

            CheckConnectivityAndToken();
        }

        public static void ConnectRealtimeChat()
        {
            try
            {
                if (PSACache.Instance.hasUserinfo)
                {
                    USNMessage.Instance.Initialize(
                                    new USNOptions(false, PSAConfig.USocketNetHostname, 10),
                                    new USNCreds(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky)
                                );
                    USNMessage.Instance.Connect();
                }
            }

            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }

        public static void CheckConnectivityAndToken()
        {
            if (PSADevice.HasInternet)
            {
                if (PSACache.Instance.hasUserinfo)
                {
                    App.Current.MainPage = new MainTabs();

                    ConnectRealtimeChat();

                    return; //Cancel all after this line.
                }

                App.Current.MainPage = new SignInPage();

                if (!PSACache.DoneWithGettingStarted)
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