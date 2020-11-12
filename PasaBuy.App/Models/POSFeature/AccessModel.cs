

using System.ComponentModel;

namespace PasaBuy.App.Models.POSFeature
{
    public class AccessModel : INotifyPropertyChanged
    {
        public string ID = string.Empty;
        public string title = string.Empty;
        public string actions = string.Empty;
        public string groups = string.Empty;
        public bool status;

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

        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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
