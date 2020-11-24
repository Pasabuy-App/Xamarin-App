using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DriverScheduleViewModel : BaseViewModel
    {
        public static ObservableCollection<DriverScheduleModel> _scheduleList;

        public ObservableCollection<DriverScheduleModel> ScheduleList
        {
            get
            {
                return _scheduleList;
            }
            set
            {
                _scheduleList = value;
                this.NotifyPropertyChanged();
            }
        }

        public ICommand EditScheduleCommand
        {
            get
            {
                return new Command<string>((x) => EditSchedule(x));
            }
        }

        private async void EditSchedule(string day)
        {
            PopupDriverEditSchedule.day = day;
            await PopupNavigation.Instance.PushAsync(new PopupDriverEditSchedule());
        }

        public DriverScheduleViewModel()
        {
            _scheduleList = new ObservableCollection<DriverScheduleModel>();
            LoadSchedule();
        }

        public static void LoadSchedule()
        {
            try
            {
                _scheduleList.Clear();
                Http.HatidPress.Schedule.Instance.Listing((bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        string mon = string.Empty;
                        string tue = string.Empty;
                        string wed = string.Empty;
                        string thu = string.Empty;
                        string fri = string.Empty;
                        string sat = string.Empty;
                        string sun = string.Empty;

                        DriverScheduleModel datas = JsonConvert.DeserializeObject<DriverScheduleModel>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string open_time = string.IsNullOrEmpty(datas.data[i].started) ? "00:00:00" : datas.data[i].started;
                            string close_time = string.IsNullOrEmpty(datas.data[i].ended) ? "00:00:00" : datas.data[i].ended;
                            DateTime open = DateTime.ParseExact(open_time, "HH:mm:ss", provider);
                            DateTime close = DateTime.ParseExact(close_time, "HH:mm:ss", provider);
                            if (datas.data[i].types == "mon")
                            {
                                mon = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "tue")
                            {
                                tue = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "wed")
                            {
                                wed = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "thu")
                            {
                                thu = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "fri")
                            {
                                fri = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "sat")
                            {
                                sat = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].types == "sun")
                            {
                                sun = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                        }
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Monday",
                            FullSchedule = mon
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Tuesday",
                            FullSchedule = tue
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Wednesday",
                            FullSchedule = wed
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Thursday",
                            FullSchedule = thu
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Friday",
                            FullSchedule = fri
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Saturday",
                            FullSchedule = sat
                        });
                        _scheduleList.Add(new DriverScheduleModel()
                        {
                            Day = "Sunday",
                            FullSchedule = sun
                        });
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
                    new Controllers.Notice.Alert("Error Code: HPV2SCH-L1DSVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2SCH-L1DSVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2SCH-L1DSVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2SCH-L1DSVM-" + err.ToString());
                }
            }
        }
    }
}
