using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupGoOnline : PopupPage
    {
        public static string _switch;
        public PopupGoOnline()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            if (_switch == "True")
            {
                Views.Navigation.MasterView._switch = false;
                Views.Navigation.MasterView._switchlist.Clear();
            }
            else
            {
                Views.Navigation.MasterView._switch = true;
                Views.Navigation.MasterView._switchlist.Clear();
            }
            PopupNavigation.Instance.PopAsync();
        }

        private void ConfirmModal(object sender, EventArgs e)
        {
            if (_switch == "True")
            {
                Views.Navigation.MasterView._switch = true;
                Views.Navigation.MasterView._switchlist.Clear();
            }
            else
            {
                Views.Navigation.MasterView._switch = false;
                Views.Navigation.MasterView._switchlist.Clear();
                //save to cache then load it to set the switch to true then insert open to database.
            }
            PopupNavigation.Instance.PopAsync();
        }
    }
}