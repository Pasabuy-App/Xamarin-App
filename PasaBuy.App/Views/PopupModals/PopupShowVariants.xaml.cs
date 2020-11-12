using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupShowVariants : PopupPage
    {
        public PopupShowVariants()
        {
            InitializeComponent();
            this.BindingContext = new PasaBuy.App.ViewModels.MobilePOS.POSVariantViewModel();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
        private void Options_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ViewModels.MobilePOS.POSVariantViewModel.InsertVariants((sender as Syncfusion.XForms.Buttons.SfRadioButton).ClassId, (sender as Syncfusion.XForms.Buttons.SfRadioButton).Text);
            }
        }
        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ViewModels.MobilePOS.POSVariantViewModel.InsertAddOns((sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId, (sender as Syncfusion.XForms.Buttons.SfCheckBox).Text);
            }
            else
            {
                ViewModels.MobilePOS.POSVariantViewModel.RemoveAddOns((sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId, (sender as Syncfusion.XForms.Buttons.SfCheckBox).Text);
            }
        }
    }
}