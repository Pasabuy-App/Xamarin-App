using Newtonsoft.Json;
using System;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignInPage" /> class.
        /// </summary>
        
       
        public SignInPage()
        {
            InitializeComponent();
        }

    }
}