using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class TypeListData
    {
        public string ID = string.Empty;
        public string title = string.Empty;
        public string info = string.Empty;
        public string status = string.Empty;
        public string created_by = string.Empty;
        public string date_created = string.Empty;
    }

    public class TypeListModel : INotifyPropertyChanged
    {
        public TypeListData[] data;

        private string id = string.Empty;

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        private string title = string.Empty;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
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
