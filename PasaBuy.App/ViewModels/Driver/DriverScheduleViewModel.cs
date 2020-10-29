using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DriverScheduleViewModel : BaseViewModel
    {
        public static ObservableCollection<DriverSchedule> _schedules;
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

        public ObservableCollection<DriverSchedule> Schedules
        {
            get 
            { 
                return _schedules; 
            }
            set 
            {
                _schedules = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public ICommand PickSchedCommand
        {
            get
            {
                return new Command<string>((x) => PickSchedule(x));
            }
        }

        private async void PickSchedule(string id)
        {
             new Alert("Demo", "Selected schedule ID: " + id, "OK");

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
            new Alert("ok", day, "ok");
            //PopupEditSchedule.day = day;
            await PopupNavigation.Instance.PushAsync(new PopupDriverEditSchedule());
        }

        public DriverScheduleViewModel()
        {
            _scheduleList = new ObservableCollection<DriverScheduleModel>();
            _schedules = new ObservableCollection<DriverSchedule>();

            for (int i = 0; i < 6; i++)
            {
                _scheduleList.Add(new DriverScheduleModel()
                {
                    Day = "Monday",
                    FullSchedule = "10:30 AM - 09:00 PM"
                });
            }
            
            for(int i = 0; i < 7; i++)
            {
                _schedules.Add(new DriverSchedule()
                {
                    ItemID = "1",
                    ScheduleStart = "10:00 AM",
                    ScheduleEnd = "09:30 PM",
                });
            }
            
        }
    }
}
