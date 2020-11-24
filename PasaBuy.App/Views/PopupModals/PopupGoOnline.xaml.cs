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
                    Views.Driver.DashboardPage.locs = false;
                }
                else
                {
                    Views.Navigation.MasterView._switch = true;
                    Views.Navigation.MasterView._switchlist.Clear();
                    Views.Driver.DashboardPage.locs = true;
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
                    UpdateOperations();
                }
                else // store is close
                {
                    Views.Navigation.MasterView._switch = false;
                    Views.Navigation.MasterView._switchlist.Clear();
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
                    UpdateAttendance();
                }
                else // mover is close
                {
                    Views.Navigation.MasterView._switch = false;
                    Views.Navigation.MasterView._switchlist.Clear();
                    //UpdateAttendance("close");
                    //save to cache then load it to set the switch to true then insert open to database.
                }
                //Local.PSACache.Instance.UserInfo.mover_status = Convert.ToBoolean(_switch);
                //Local.PSACache.Instance.SaveUserData();
            }

            PopupNavigation.Instance.PopAsync();
        }

        public void UpdateOperations()
        {
            try
            {
                Http.MobilePOS.Operation.Instance.Update((bool success, string data) =>
                {
                    if (!success)
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        Views.Navigation.MasterView._switch = false;
                        Views.Navigation.MasterView._switchlist.Clear();
                        //Local.PSACache.Instance.UserInfo.mover_status = Convert.ToBoolean(_switch);
                        //Local.PSACache.Instance.SaveUserData();
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2OPE-I1GO", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2OPE-II1GO1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2OPE-I1GO.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2OPE-I1GO-" + err.ToString());
                }
            }
        }

        public void UpdateAttendance()
        {
            try
            {
                Http.HatidPress.MoverData.Instance.Attendance((bool success, string data) =>
                {
                    if (!success)
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        Views.Navigation.MasterView._switch = false;
                        Views.Navigation.MasterView._switchlist.Clear();
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2MVR-A1GO", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2MVR-A1GO-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2MVR-A1GO.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2MVR-A1GO-" + err.ToString());
                }
            }
        }
    }
}