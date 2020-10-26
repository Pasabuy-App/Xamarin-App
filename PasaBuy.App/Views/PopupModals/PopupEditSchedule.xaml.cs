using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditSchedule : PopupPage
    {
        public static string day;
        public PopupEditSchedule()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OKModal(object sender, EventArgs e)
        {
            string days = string.Empty;
            if (day == "Monday")
            {
                days = "mon";
            }
            if (day == "Tuesday")
            {
                days = "tue";
            }
            if (day == "Wednesday")
            {
                days = "wed";
            }
            if (day == "Thursday")
            {
                days = "thu";
            }
            if (day == "Friday")
            {
                days = "fri";
            }
            if (day == "Saturday")
            {
                days = "sat";
            }
            if (day == "Sunday")
            {
                days = "sun";
            }
            days = day != "Monday" ? day != "Tuesday" ? day != "Wednesday" ? day != "Thursday" ? day != "Friday" ? day != "Saturday" ? "sun" : "sat" : "fri" : "thu" : "wed" : "tue" : "mon";
            //new Alert(days, Open.Time + ". ." + Close.Time, day);
            if (Open.Time.ToString() == "00:00:00" || Close.Time.ToString() == "00:00:00")
            {
                new Alert("Notice to User", "Please enter opening or closing time.", "Try Again");
            }
            else
            {
                try
                {
                    TindaPress.Schedule.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, days, Open.Time.ToString(), Close.Time.ToString(), (bool success, string data) =>
                    {
                        if (success)
                        {
                            ScheduleViewModel.LoadSchedule();
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception ex)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                }
            }
        }

    }
}