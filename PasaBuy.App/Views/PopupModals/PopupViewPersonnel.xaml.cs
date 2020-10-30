using PasaBuy.App.Controllers.Notice;
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
        public static string id;
        public static string name;
        public static string position;
        public static string date;
        public static string status;
        public static string activated;
        public PopupViewPersonnel()
        {
            InitializeComponent();
            this.BindingContext = new PersonnelsViewModel();
            xName.Text = name;
            xPostition.Text = position;
            xDate.Text = date;
            xStatus.Text = status;
            xActivate.Text = activated == "true" ? "Deactivate" : "Activate";
            if (status == "Active")
            {
                xActivate.IsVisible = true;
            }
            else
            {
                xActivate.IsVisible = false;
            }
        }

        private async void CloseModal(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void xActivate_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (activated == "true")
                {
                    new Alert("Success", "Activated!", "OK");
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                    new Alert("Success", "Deactivated!", "OK");
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}