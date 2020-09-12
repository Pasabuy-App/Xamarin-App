using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Notification
{
    public class TaskNotification
    {
        public string id = string.Empty;
        public string username = string.Empty;
        public string backgroundcolor = string.Empty;
        public string description = string.Empty;
        public string detail = string.Empty;
        public string taskid = string.Empty;
        public string time = string.Empty;
        public Boolean isread = false;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("IsRead");
            }
        }
        public Boolean IsRead
        {
            get { return isread; }
            set
            {
                isread = value;
                OnPropertyChanged("IsRead");
            }
        }
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public string TaskID
        {
            get { return taskid; }
            set
            {
                taskid = value;
                OnPropertyChanged("TaskID");
            }
        }

        public string Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                OnPropertyChanged("Detail");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string BackgroundColor
        {
            get { return backgroundcolor; }
            set
            {
                backgroundcolor = value;
                OnPropertyChanged("BackgroundColor");
            }
        }

        public string UserName
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("UserName");
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
