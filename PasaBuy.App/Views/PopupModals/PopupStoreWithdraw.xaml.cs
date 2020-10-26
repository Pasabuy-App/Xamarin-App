using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupStoreWithdraw : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupStoreWithdraw()
        {
            InitializeComponent();
        }

        private async void CancelModal(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}