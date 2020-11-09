using Forms9Patch;
using PasaBuy.App.Local;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupConfirmDeleteDocumentStore : PopupPage
    {
        public static string hash_id = string.Empty;
        public PopupConfirmDeleteDocumentStore()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private  void ConfirmLogout(object sender, EventArgs e)
        {
            try
            {
                Http.TindaPress.Document.Instance.Delete(hash_id, (bool success, string data) =>
                {
                    if (success)
                    {
                        DocumentViewModel.RefreshData();
                        PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        App.Current.MainPage.DisplayAlert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });


            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2DOC-D1PUCDTPV2DOC-D1PUCD.", "OK");
            }

        }
    }

}