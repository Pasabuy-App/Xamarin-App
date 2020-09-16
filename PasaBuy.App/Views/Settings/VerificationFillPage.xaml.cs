using PasaBuy.App.Controllers.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFillPage : ContentPage
    {
        public VerificationFillPage()
        {
            InitializeComponent();
        }
        private void NextButtonClicked(object sender, EventArgs e)
        {   
            //TO DO :: Add validations
            if (String.IsNullOrEmpty(FirstNameEntry.Text))
            {   
                new Alert("Failed", "Please complete all fields", "Ok");
            }
            else
            {
                //Save this to database

                Navigation.PushModalAsync(new VerificationFinal());
            }
        }
    }
}