using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditPersonnel : PopupPage
    {
        public static string name;
        public static string role_id;
        public static string id;
        public PopupEditPersonnel()
        {
            InitializeComponent();
            PersonnelName.Text = name;
            this.BindingContext = new RolesViewModel();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}