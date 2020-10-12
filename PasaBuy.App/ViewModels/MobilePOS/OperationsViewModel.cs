using MobilePOS;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OperationsViewModel : BaseViewModel
    {
        public static ObservableCollection<Operations> _daysOfTheWeek;

        public static ObservableCollection<Operations> _scheduleList;

        public static ObservableCollection<Operations> _operationsList;


        public bool is_online;

        public ICommand EditScheduleCommand
        {
            get
            {
                return new Command<string>((x) => EditSchedule(x));
            }
        }

        private async void EditSchedule(string id)
        {
            new Alert("ok", id, "ok");
            await PopupNavigation.Instance.PushAsync(new PopupEditSchedule());
        }

        public bool IsOnline
        {
            get
            {
                return is_online;
            }
            set
            {
                is_online = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Operations> DaysOfTheWeek
        {
            get
            {
                return _daysOfTheWeek;
            }
            set
            {
                _daysOfTheWeek = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Operations> OperationsList
        {
            get
            {
                return _operationsList;
            }
            set
            {
                _operationsList = value;
                this.NotifyPropertyChanged();
            }
        }


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

        public OperationsViewModel()
        {
            this.IsOnline = true;

            this.DaysOfTheWeek = new ObservableCollection<Operations>()
            {
                new Operations
                {
                    Day = "Monday"

                },
                new Operations
                {
                    Day = "Tuesday"

                },
                new Operations
                {
                    Day = "Wednesday"

                },
                new Operations
                {
                    Day = "Thursday"

                },
                new Operations
                {
                    Day = "Friday"

                },
                new Operations
                {
                    Day = "Saturday"

                },
                new Operations
                {
                    Day = "Sunday"

                }
            };

            this.ScheduleList = new ObservableCollection<Operations>()
            {
                new Operations
                {
                    Day = "Monday",
                    Opening = "11:00 AM",
                    Closing = "10:00 PM",
                    FullSchedule = "10:00 AM - 10:00 PM"
                },
                new Operations
                {
                    Day = "Tuesday",
                    Opening = "10:00 AM",
                    Closing = "11:30 PM",
                    FullSchedule = "10:00 AM - 11:30 PM"
                },
                new Operations
                {
                    Day = "Thursday",
                    Opening = "10:30 AM",
                    Closing = "10:00 PM",
                    FullSchedule = "10:30 AM - 10:00 PM"
                },
                new Operations
                {
                    Day = "Friday",
                    Opening = "11:00 AM",
                    Closing = "09:00 PM",
                    FullSchedule = "11:00 AM - 9:00 PM"
                },
                new Operations
                {
                    Day = "Saturday",
                    Opening = "11:30 AM",
                    Closing = "11:00 PM",
                    FullSchedule = "11:30 AM - 11:00 PM"
                },
                new Operations
                {
                    Day = "Saturday",
                    Opening = "11:30 AM",
                    Closing = "11:00 PM",
                    FullSchedule = "11:30 AM - 11:00 PM"
                },
                new Operations
                {
                    Day = "Saturday",
                    Opening = "11:30 AM",
                    Closing = "11:00 PM",
                    FullSchedule = "11:30 AM - 11:00 PM"
                },
            };

            this.OperationsList = new ObservableCollection<Operations>()
            {
                new Operations
                {
                    Date = "Date: Oct. 5, 2020",
                    TotalSales = "Total Sales: 23,000.50"

                },
                new Operations
                {
                    Date = "Date: Oct. 5, 2020",
                    TotalSales = "Total Sales: 23,000.50"

                },
                new Operations
                {
                    Date = "Date: Oct. 5, 2020",
                    TotalSales = "Total Sales: 23,000.50"

                },
                new Operations
                {
                    Date = "Date: Oct. 5, 2020",
                    TotalSales = "Total Sales: 23,000.50"

                }

            };

        }

    }
}
