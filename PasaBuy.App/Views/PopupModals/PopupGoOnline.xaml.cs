using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;
using MobilePOS;
using PasaBuy.App.Local;

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
            if (Views.Navigation.MasterView.MyType == "store")
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
            }

            PopupNavigation.Instance.PopAsync();
        }

        private void ConfirmModal(object sender, EventArgs e)
        {
            if (Views.Navigation.MasterView.MyType == "store")
            {
                if (_switch == "True") // store is open
                {
                    //new Controllers.Notice.Alert("Switch", "Store is open", "Ok"); // insert
                    Views.Navigation.MasterView._switch = true;
                    Views.Navigation.MasterView._switchlist.Clear();
                    UpdateOperations("open");
                }
                else // store is close
                {
                    //new Controllers.Notice.Alert("Switch", "Store is close", "Ok"); // update
                    Views.Navigation.MasterView._switch = false;
                    Views.Navigation.MasterView._switchlist.Clear();
                    UpdateOperations("close");
                    //save to cache then load it to set the switch to true then insert open to database.
                }
                Local.PSACache.Instance.UserInfo.store_status = Convert.ToBoolean(_switch);
                Local.PSACache.Instance.SaveUserData();
            }

            PopupNavigation.Instance.PopAsync();
        }

        public void UpdateOperations(string status) // status = open or close
        {
            try
            {
                Operation.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, status, (bool success, string data) =>
                {
                    if (!success)
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
    }
}