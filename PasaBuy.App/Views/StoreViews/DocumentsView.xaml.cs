using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
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
        }

        private async void AddDocumentClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddDocument());
        }
 
    }
}