using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddCategory : PopupPage
    {
        public static string catid = "0";
        public static string catname;
        public static string catinfo;
        public PopupAddCategory()
        {
            InitializeComponent();
            if (catid != "0")
            {
                CatTitle.Text = "Edit Category";
                CatName.Text = catname;
                Description.Text = catinfo;
            }
            else
            {
                CatTitle.Text = "Add Category";
                CatName.Text = string.Empty;
                Description.Text = string.Empty;
            }
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void Insert_UpdateModal(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Description.Text) || !string.IsNullOrWhiteSpace(CatName.Text))
                {
                    if (catid == "0")
                    {
                        Http.TindaPress.Category.Instance.Insert(CatName.Text, Description.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                CategoryViewModel.RefreshCategory("");
                                PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                    else
                    {
                        Http.TindaPress.Category.Instance.UpdateDelete(catid, "", CatName.Text, Description.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                CategoryViewModel.RefreshCategory("");
                                PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-IU1PUAC", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-IU1PUAC-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-IU1PUAC.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-IU1PUAC-" + err.ToString());
                }
            }
        }
    }
}