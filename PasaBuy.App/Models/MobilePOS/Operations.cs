using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class Operations
    {
        private string day = string.Empty;


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
