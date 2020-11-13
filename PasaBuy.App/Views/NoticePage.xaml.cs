using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoticePage : ContentPage
    {
        public NoticePage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void VerifyBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PasaBuy.App.Views.Settings.VerifyAccountFront());
        }
    }
}