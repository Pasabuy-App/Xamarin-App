﻿using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDriverEditSchedule : PopupPage
    {
        public static string day;
        public PopupDriverEditSchedule()
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
            days = day != "Monday" ? day != "Tuesday" ? day != "Wednesday" ? day != "Thursday" ? day != "Friday" ? day != "Saturday" ? "sun" : "sat" : "fri" : "thu" : "wed" : "tue" : "mon";
            if (Open.Time.ToString() == "00:00:00" || Close.Time.ToString() == "00:00:00")
            {
                new Controllers.Notice.Alert("Notice to User", "Please enter opening or closing time.", "Try Again");
            }
            else
            {
                try
                {
                    Http.HatidPress.Schedule.Instance.Insert(days, Open.Time.ToString(), Close.Time.ToString(), (bool success, string data) =>
                    {
                        if (success)
                        {
                            ViewModels.Driver.DriverScheduleViewModel.LoadSchedule();
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception err)
                {
                    if (PSAConfig.isDebuggable)
                    {
                        new Controllers.Notice.Alert("Error Code: HPV2SCH-I1PUDES", err.ToString(), "OK");
                        Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2SCH-I1PUDES-" + err.ToString());
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2SCH-I1PUDES.", "OK");
                        Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2SCH-I1PUDES-" + err.ToString());
                    }
                }
            }
        }
    }
}