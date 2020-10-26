using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddVariants : PopupPage
    {
        public int isBase = 1;
        public PopupAddVariants()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OKModal(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Name.Text))
                {
                    TindaPress.Variant.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "0", ProductVariants.product_id, isBase.ToString(), "", Name.Text, Description.Text, (bool success, string data) =>
                    {
                        if (success)
                        {
                            VariantsViewModel.LoadVariants(ProductVariants.product_id);
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                //checkBox.Text = "Checked State";
                isBase = 1;
            }
            else if (e.IsChecked.HasValue && !e.IsChecked.Value)
            {
                //checkBox.Text = "Unchecked State";
                isBase = 0;
            }
        }
    }
}