using System.ComponentModel;

namespace PasaBuy.App.Models.POSFeature
{
    public class OperationModel : INotifyPropertyChanged
    {
        public OperationsData[] data;

        private string day = string.Empty;
        private string id = string.Empty;
        private string opening = string.Empty;
        private string closing = string.Empty;
        private string full_schedule = string.Empty;
        private string date = string.Empty;
        private string total_sales;
        private string date_time = string.Empty;

        private bool is_online;

        public string Date_Time
        {
            get
            {
                return date_time;
            }
            set
            {
                date_time = value;
                OnPropertyChanged("Date_Time");
            }
        }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string TotalSales
        {
            get
            {
                return total_sales;
            }
            set
            {
                total_sales = value;
                OnPropertyChanged("TotalSales");
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

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

    public class OperationsData
    {
        public string ID = string.Empty;
        public string types = string.Empty;
        public string started = string.Empty;
        public string ended = string.Empty;
        public string total_sale = string.Empty;
        public string date = string.Empty;
        public string date_open = string.Empty;
        public string date_close = string.Empty;
    }
}
