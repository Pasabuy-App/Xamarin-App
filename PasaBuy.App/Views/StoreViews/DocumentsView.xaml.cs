using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsView : ContentPage
    {
        public DocumentsView()
        {
            InitializeComponent();
            AddDocumentButton.Clicked += AddDocumentClicked;
        }

        private void AddDocumentClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAddDocument());
        }

        private async void ManagementItem_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("Delete Document?", "Are you sure to delete this?", "Yes", "No");
            if (answer)
            {
                try
                {
                    var item = e.ItemData as DocumentData; //item.ID;
                    TindaPress.Document.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, item.ID, (bool success, string data) =>
                    {
                        if (success)
                        {
                            DocumentViewModel.documentList.Clear();
                            DocumentViewModel.LoadData();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20416.", "OK");
                }
            }
        }
    }
}