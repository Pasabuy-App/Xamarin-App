using PasaBuy.App.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasabuyPlusPage : ContentPage
    {
        public PasabuyPlusPage()
        {
            InitializeComponent();
            this.BindingContext = new PasabuyPlusViewModel();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}