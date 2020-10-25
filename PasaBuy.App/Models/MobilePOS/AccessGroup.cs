using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class AccessGroup : INotifyPropertyChanged
    {
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
