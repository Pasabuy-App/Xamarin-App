using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewPersonnel : PopupPage
    {
        public PopupViewPersonnel()
        {
            InitializeComponent();
            this.BindingContext = new PersonnelsViewModel();
        }

        private async void CloseModal(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}