using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
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
        public int count = 0;
        public DocumentsView()
        {
            InitializeComponent();
            this.BindingContext = new DocumentViewModel();
            AddDocumentButton.Clicked += AddDocumentClicked;
        }

        private void AddDocumentClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAddDocument());
        }
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    count = 1;
                    bool answer = await DisplayAlert("Delete Address?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                            var btn = sender as Grid;
                            if (MasterView.MyType == "store")
                            {
                                TindaPress.Document.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, btn.ClassId, (bool success, string data) =>
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
                            if (MasterView.MyType == "mover")
                            {
                                HatidPress.Documents.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
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
                    }
                    await Task.Delay(200);
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
            /*private async void ManagementItem_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as DocumentData; //item.ID;
            bool answer = await DisplayAlert("Delete Document?", "Are you sure to delete this?", "Yes", "No");
            if (answer)
            {
                try
                {
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20416.", "OK");
                }
            }
        }*/
    }
}