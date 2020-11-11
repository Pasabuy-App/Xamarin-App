using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditRole : Rg.Plugins.Popup.Pages.PopupPage
    {
        public static string title;
        public static string description;
        public PopupEditRole()
        {
            InitializeComponent();
            this.BindingContext = new AccessViewModel();
            RoleName.Text = title;
            Description.Text = description;
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}