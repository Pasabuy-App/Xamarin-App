using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Driver;
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

        public DriverScheduleViewModel()
        {
            _schedules = new ObservableCollection<DriverSchedule>();
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
