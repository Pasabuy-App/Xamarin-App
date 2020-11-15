using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RolesView : ContentPage
    {
        public int count = 0;
        public RolesView()
        {
            InitializeComponent();
            this.BindingContext = new RolesViewModel();
            xAdd.IsEnabled = false;
            CheckPermission();
        }

        public void CheckPermission()
        {
            bool edit = false;
            bool delete = false;
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_role")
                {
                    edit = true;
                }
                if (per.action == "delete_role")
                {
                    delete = true;
                }
            }
            if (!delete && !edit)
            {
                ListView.AllowSwiping = false;
            }
            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "add_role"))
            {
                xAdd.IsEnabled = true;
            }
        }
    }
}