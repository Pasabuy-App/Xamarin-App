using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class Operations
    {
        private string day = string.Empty;
        private string opening = string.Empty;
        private string closing = string.Empty;
        private string full_schedule = string.Empty;
        private bool is_online;




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

        public bool IsOnline
        {
            get
            {
                return is_online;
            }
            set
            {
                is_online = value;
                OnPropertyChanged("IsOnline");
            }
        }

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
        public string Opening
        {
            get
            {
                return opening;
            }
            set
            {
                opening = value;
                OnPropertyChanged("Opening");
            }
        }
        public string Closing
        {
            get
            {
                return closing;
            }
            set
            {
                closing = value;
                OnPropertyChanged("Closing");
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
