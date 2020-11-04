using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
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
        private async void Submit()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                GetStarted = false;
                Local.PSACache.Instance.UserInfo.store_schedule = true;
                Local.PSACache.Instance.SaveUserData();
                await Task.Delay(300);
                IsRunning = false;
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
            if (!IsRunning)
            {
                IsRunning = true;
                PopupEditSchedule.day = day;
                await PopupNavigation.Instance.PushAsync(new PopupEditSchedule());
                IsRunning = false;
            }
        }

        public bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public static ObservableCollection<Models.POSFeature.OperationModel> _scheduleList;
        public ObservableCollection<Models.POSFeature.OperationModel> ScheduleList
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
            _scheduleList = new ObservableCollection<Models.POSFeature.OperationModel>();
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

        public void LoadSchedule()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _scheduleList.Clear();
                    Http.MobilePOS.Schedule.Instance.Listing((bool success, string data) =>
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

                            Models.POSFeature.OperationModel datas = JsonConvert.DeserializeObject<Models.POSFeature.OperationModel>(data);

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
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Monday",
                                FullSchedule = mon
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Tuesday",
                                FullSchedule = tue
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Wednesday",
                                FullSchedule = wed
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Thursday",
                                FullSchedule = thu
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Friday",
                                FullSchedule = fri
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Saturday",
                                FullSchedule = sat
                            });
                            _scheduleList.Add(new Models.POSFeature.OperationModel()
                            {
                                Day = "Sunday",
                                FullSchedule = sun
                            });
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2SCH-L1SVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshSchedule()
        {
            try
            {
                _scheduleList.Clear();
                Http.MobilePOS.Schedule.Instance.Listing((bool success, string data) =>
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

                        Models.POSFeature.OperationModel datas = JsonConvert.DeserializeObject<Models.POSFeature.OperationModel>(data);

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
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Monday",
                            FullSchedule = mon
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Tuesday",
                            FullSchedule = tue
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Wednesday",
                            FullSchedule = wed
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Thursday",
                            FullSchedule = thu
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Friday",
                            FullSchedule = fri
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
                        {
                            Day = "Saturday",
                            FullSchedule = sat
                        });
                        _scheduleList.Add(new Models.POSFeature.OperationModel()
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2SCH-L2SVM.", "OK");
            }
        }
    }
}
