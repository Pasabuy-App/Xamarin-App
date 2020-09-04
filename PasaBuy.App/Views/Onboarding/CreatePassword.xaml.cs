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
    public partial class CreatePassword : ContentPage
    {
        public CreatePassword()
        {
            InitializeComponent();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            Users.Instance.NewPassword(VerifyAccountVar.ak, VerifyAccountVar.un, Password.Text, ConfirmPassowrd.Text, (bool success, string data) =>
           {
               if (success)
               {
                   Application.Current.MainPage = new MainTabs();
                }
               else
               {
                   new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
               }
           });
        }
    }
}