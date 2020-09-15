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
    public partial class VerifyAccountFront : ContentPage
    {
        public VerifyAccountFront()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();

        }

        public void StartVerification(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TakeIdPhotoPage());

        }

        
    }
}