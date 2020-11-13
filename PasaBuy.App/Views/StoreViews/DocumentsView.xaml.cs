using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
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
            xAdd.IsEnabled = false;
            CheckPermission();
        }

        public void CheckPermission()
        {
            bool edit = false;
            bool delete = false;
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_document")
                {
                    edit = true;
                }
                if (per.action == "delete_document")
                {
                    delete = true;
                }
            }
            if (!delete && !edit)
            {
                DocumentList.AllowSwiping = false;
            }
            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "add_document"))
            {
                xAdd.IsEnabled = true;
            }
        }

        private async void AddDocumentClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddDocument());
        }
 
    }
}