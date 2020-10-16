using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class ScheduleViewModel : BaseViewModel
    {
        public bool _GetStarted;
        public bool GetStarted
        {
            get
            {
                return _GetStarted;
            }
            set
            {
                _GetStarted = value;
                this.NotifyPropertyChanged();
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                return new Command<string>((x) => Submit());
            }
        }
        private  void Submit()
        {
            GetStarted = false;
            Local.PSACache.Instance.UserInfo.store_schedule = true;
            Local.PSACache.Instance.SaveUserData();
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
            //new Alert("ok", day, "ok");
            PopupEditSchedule.day = day;
            await PopupNavigation.Instance.PushAsync(new PopupEditSchedule());
        }

        public static ObservableCollection<Operations> _scheduleList;
        public ObservableCollection<Operations> ScheduleList
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
        public ScheduleViewModel()
        {
            _scheduleList = new ObservableCollection<Operations>();
            LoadSchedule();
            if (Local.PSACache.Instance.UserInfo.store_schedule)
            {
                GetStarted = false;
            }
            else
            {
                GetStarted = true;
            }
        }
        public static void LoadSchedule()
        {
            try
            {
                _scheduleList.Clear();
                TindaPress.Schedule.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, (bool success, string data) =>
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

                        Operations datas = JsonConvert.DeserializeObject<Operations>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string open_time = string.IsNullOrEmpty(datas.data[i].open) ? "00:00:00" : datas.data[i].open;
                            string close_time = string.IsNullOrEmpty(datas.data[i].close) ? "00:00:00" : datas.data[i].close;
                            DateTime open = DateTime.ParseExact(open_time, "HH:mm:ss", provider);
                            DateTime close = DateTime.ParseExact(close_time, "HH:mm:ss", provider);
                            if (datas.data[i].type == "mon")
                            {
                                mon = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].type == "tue")
                            {
                                tue = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                            }
                            if (datas.data[i].type == "wed")
                            {
                                wed = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                                //wed = datas.data[i].open + " AM " + datas.data[i].close + " PM";
                            }
                            if (datas.data[i].type == "thu")
                            {
                                thu = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                                //thu = datas.data[i].open + " AM " + datas.data[i].close + " PM";
                            }
                            if (datas.data[i].type == "fri")
                            {
                                fri = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                                //fri = datas.data[i].open + " AM " + datas.data[i].close + " PM";
                            }
                            if (datas.data[i].type == "sat")
                            {
                                sat = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                                //sat = datas.data[i].open + " AM " + datas.data[i].close + " PM";
                            }
                            if (datas.data[i].type == "sun")
                            {
                                sun = open.ToString("hh:mm tt") + " - " + close.ToString("hh:mm tt");
                                //sun = datas.data[i].open + " AM " + datas.data[i].close + " PM";
                            }
                        }
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Monday",
                            FullSchedule = mon
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Tuesday",
                            FullSchedule = tue
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Wednesday",
                            FullSchedule = wed
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Thursday",
                            FullSchedule = thu
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Friday",
                            FullSchedule = fri
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Saturday",
                            FullSchedule = sat
                        });
                        _scheduleList.Add(new Operations()
                        {
                            Day = "Sunday",
                            FullSchedule = sun
                        });
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
    }
}
