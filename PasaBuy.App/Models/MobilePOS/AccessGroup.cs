using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class AccessData
    {
        public string name = string.Empty;
        public ObservableCollection<AccessModel> access { get; set; }
    }
    public class AccessGroup : INotifyPropertyChanged
    {
        public AccessData[] data;

        private string group_name = string.Empty;

        public ObservableCollection<AccessModel> AccessList { get; set; }

        public string GroupName
        {
            get
            {
                return group_name;
            }
            set
            {
                group_name = value;
                OnPropertyChanged("GroupName");
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
