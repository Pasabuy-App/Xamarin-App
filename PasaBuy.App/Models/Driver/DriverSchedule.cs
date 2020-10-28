using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class DriverSchedule
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string item_id = string.Empty;
        private string schedule_start = string.Empty;
        private string schedule_end = string.Empty;

        public string ItemID
        {
            get { return item_id; }
            set
            {
                item_id = value;
                OnPropertyChanged("ItemID");
            }
        }

        public string ScheduleStart
        {
            get { return schedule_start; }
            set
            {
                schedule_start = value;
                OnPropertyChanged("ScheduleStart");
            }
        }

        public string ScheduleEnd
        {
            get { return schedule_end; }
            set
            {
                schedule_end = value;
                OnPropertyChanged("ScheduleEnd");
            }
        }
    }
}
