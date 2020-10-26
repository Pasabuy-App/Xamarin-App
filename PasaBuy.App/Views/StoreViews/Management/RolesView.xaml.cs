using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RolesView : ContentPage
    {
        public RolesView()
        {
            InitializeComponent();
            this.BindingContext = new RolesViewModel();
        }

        private async void Update_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditRole());
        }

        private void Delete_Tapped(object sender, EventArgs e)
        {

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddRole());
        }
    }
}