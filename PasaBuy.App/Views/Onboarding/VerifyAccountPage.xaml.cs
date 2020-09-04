using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataVice;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyAccountPage : ContentPage
    {
        public VerifyAccountPage()
        {
            InitializeComponent();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            Users.Instance.Activate(ActivationKey.Text, Username.Text, (bool success, string data) =>
            {
                if (success)
                {
                    VerifyAccountVar.ak = ActivationKey.Text;
                    VerifyAccountVar.un = Username.Text;
                    Application.Current.MainPage = new CreatePassword();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });

        }
    }
}