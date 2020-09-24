using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class CategoryData : INotifyPropertyChanged
    {
        private string id = string.Empty;
        private string title = string.Empty;
        private string info = string.Empty;
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
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
