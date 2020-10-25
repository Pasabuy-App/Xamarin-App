using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class AccessModel : INotifyPropertyChanged
    {
        private string access_name = string.Empty;
        private string access_id = string.Empty;


        public string AccessName
        {
            get
            {
                return access_name;
            }
            set
            {
                access_name = value;
                OnPropertyChanged("AccessName");
            }
        }

        public string AccessId
        {
            get
            {
                return access_id;
            }
            set
            {
                access_id = value;
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
