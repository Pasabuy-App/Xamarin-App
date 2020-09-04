using DataVice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyResetPage : ContentPage
    {
        public VerifyResetPage()
        {
            InitializeComponent();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            Users.Instance.Activate(ActivationKey.Text, VerifyAccountVar.un, (bool success, string data) =>
            {
                if (success)
                {
                    VerifyAccountVar.ak = ActivationKey.Text;
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