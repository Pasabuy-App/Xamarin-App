using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class AccessModel : INotifyPropertyChanged
    {
        public string ID = string.Empty;
        public string title = string.Empty;
        public string actions = string.Empty;
        public string groups = string.Empty;

        public string AccessActions
        {
            get
            {
                return actions;
            }
            set
            {
                actions = value;
                OnPropertyChanged("AccessActions");
            }
        }
        public string AccessName
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("AccessName");
            }
        }

        public string AccessId
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
                OnPropertyChanged("AccessId");
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
