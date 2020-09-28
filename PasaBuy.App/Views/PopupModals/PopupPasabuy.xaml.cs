using PasaBuy.App.Views.Posts;
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
    public partial class PopupPasabuy : PopupPage
    {
        public PopupPasabuy()
        {
            InitializeComponent();
        }

        private void GoToPasabuy(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            Navigation.PushModalAsync(new PasabayPage());

        }
        private void GoToPabili(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            Navigation.PushModalAsync(new PabiliPage());
        }

        private void GoToPahatid(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            Navigation.PushModalAsync(new PahatidPage());
        }
    }
}