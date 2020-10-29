using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class DriverScheduleModel
    {
        private string time_start = string.Empty;
        private string time_end = string.Empty;
        private string full_schedule = string.Empty;
        private string day = string.Empty;

        public string FullSchedule
        {
            get
            {
                return full_schedule;
            }
            set
            {
                full_schedule = value;
                OnPropertyChanged("FullSchedule");
            }
        }
        public string TimeStart
        {
            get
            {
                return time_start;
            }
            set
            {
                time_start = value;
                OnPropertyChanged("TimeStart");
            }
        }
        public string TimeEnd
        {
            get
            {
                return time_end;
            }
            set
            {
                time_end = value;
                OnPropertyChanged("TimeEnd");
            }
        }

        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }


    

}
