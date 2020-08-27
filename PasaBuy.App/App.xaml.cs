using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Onboarding;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public static string BaseRootUrl { get; } = "http://10.0.2.2";

        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjYwNjY0QDMxMzgyZTMxMmUzMGlJTnpSZVBDTG4raGhBaU5DOVRFbFBKWmxRYVVHd1hxNUx2cXcrZFliMmc9");

            UserPrefs.Instance.Initialize();

            MainPage = new NavigationPage(new MyProfile());
        }

        /// <summary>
        /// Check if the user skip or completed the Getting Started.
        /// </summary>
        public static bool DoneWithGettingStarted
        {
            get
            {
                return Preferences.ContainsKey("ReturnUser") ? true : false;
            }
        }

        /// <summary>
        /// Set and save the action of the user toward the Getting Started page.
        /// </summary>
        /// <param name="isSkipped"></param>
        public static void SetGettingStartedAction(bool isSkipped)
        {
            Preferences.Set("ReturnUser", true);
        }

        /// <summary>
        /// Check if the application is able to reach the internet.
        /// </summary>
        public static bool HasInternet
        {
            get
            {
                return Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
            }
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
