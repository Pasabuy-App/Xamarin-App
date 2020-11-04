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
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                string days = string.Empty;
                days = day != "Monday" ? day != "Tuesday" ? day != "Wednesday" ? day != "Thursday" ? day != "Friday" ? day != "Saturday" ? "sun" : "sat" : "fri" : "thu" : "wed" : "tue" : "mon";
                if (Open.Time.ToString() == "00:00:00" || Close.Time.ToString() == "00:00:00")
                {
                    new Alert("Notice to User", "Please enter opening or closing time.", "Try Again");
                    IsRunning.IsRunning = false;
                }
                else
                {
                    try
                    {
                        Http.MobilePOS.Schedule.Instance.Insert(days, Open.Time.ToString(), Close.Time.ToString(), async (bool success, string data) =>
                        {
                            if (success)
                            {
                                ScheduleViewModel.RefreshSchedule();
                                await PopupNavigation.Instance.PopAsync();
                                IsRunning.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning.IsRunning = false;
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2SCH-I1ES.", "OK");
                        IsRunning.IsRunning = false;
                    }
                }
            }
        }

    }
}