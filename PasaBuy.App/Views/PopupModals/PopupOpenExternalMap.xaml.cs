using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupOpenExternalMap : PopupPage
    {
        public static string item_id = string.Empty;

        public PopupOpenExternalMap()
        {
            InitializeComponent();
        }

        private void OKModal_Clicked(object sender, EventArgs e)
        {
            StartDeliveryPage.OpenMapApp();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            StartDeliveryPage.drawpath = true;
            PopupNavigation.Instance.PopAsync();
        }
    }
}