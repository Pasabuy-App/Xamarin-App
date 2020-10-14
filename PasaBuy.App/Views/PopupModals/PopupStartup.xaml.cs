using PasaBuy.App.Views.Settings;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupStartup : PopupPage
    {
        public PopupStartup()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
       
        }

        private async void SkipVerify(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void VerifyNow(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushModalAsync(new VerifyAccountFront());
        }
    }
}