using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddRole : PopupPage
    {
        public PopupAddRole()
        {
            InitializeComponent();
            this.BindingContext = new AccessViewModel();

        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Access_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                // Insert
                AccessViewModel.Insert((sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId);
            }
            else
            {
                // Remove
                AccessViewModel.Remove((sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId);
            }
        }
    }
}