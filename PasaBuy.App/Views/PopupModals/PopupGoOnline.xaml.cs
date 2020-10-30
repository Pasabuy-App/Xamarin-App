using MobilePOS;
using PasaBuy.App.Local;
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

            if (Views.Navigation.MasterView.MyType == "mover")
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
                    Views.Navigation.MasterView._switch = true;
                    Views.Navigation.MasterView._switchlist.Clear();
                    UpdateOperations("open");
                }
                else // store is close
                {
                    Views.Navigation.MasterView._switch = false;
                    Views.Navigation.MasterView._switchlist.Clear();
                    UpdateOperations("close");
                    //save to cache then load it to set the switch to true then insert open to database.
                }
                Local.PSACache.Instance.UserInfo.store_status = Convert.ToBoolean(_switch);
                Local.PSACache.Instance.SaveUserData();
            }

            if (Views.Navigation.MasterView.MyType == "mover")
            {
                if (_switch == "True") // mover is open
                {
                    Views.Navigation.MasterView._switch = true;
                    Views.Navigation.MasterView._switchlist.Clear();
                    //UpdateAttendance("open");
                }
                else // mover is close
                {
                    Views.Navigation.MasterView._switch = false;
                    Views.Navigation.MasterView._switchlist.Clear();
                    //UpdateAttendance("close");
                    //save to cache then load it to set the switch to true then insert open to database.
                }
                Local.PSACache.Instance.UserInfo.mover_status = Convert.ToBoolean(_switch);
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

        public void UpdateAttendance(string status)
        {
            try
            {
                HatidPress.Rider.Instance.UpdateAttendance(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, status, "", (bool success, string data) =>
                {

                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public void UploadLocation()
        {

        }
    }
}