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
    public partial class EditAddressPage : ContentPage
    {
        public EditAddressPage()
        {
            InitializeComponent();
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}