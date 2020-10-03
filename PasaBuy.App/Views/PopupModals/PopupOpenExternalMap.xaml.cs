using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Views.Driver;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupOpenExternalMap : PopupPage
    {
        public static string item_id = string.Empty;

        public PopupOpenExternalMap()
        {
            InitializeComponent();
        }

        private  void OKModal_Clicked(object sender, EventArgs e)
        {
            StartDeliveryPage.OpenMapApp();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            StartDeliveryPage.drawpath = true;
            PopupNavigation.Instance.PopAsync();
        }
    }
}