using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddPersonnel : PopupPage
    {
        public PopupAddPersonnel()
        {
            InitializeComponent();
            this.BindingContext = new PersonnelsViewModel();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}