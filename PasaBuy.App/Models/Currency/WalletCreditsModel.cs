using System;
using System.ComponentModel;

namespace PasaBuy.App.Models.Currency
{
    public class WalletCreditsModel : INotifyPropertyChanged
    {
        public string balance;

        public WalletData[] data;
        public class WalletData
        {
            public string name = string.Empty;
            public string avatar = string.Empty;
            public string public_key = string.Empty;
            public string currency_id = string.Empty;
            public string remarks = string.Empty;
            public string date_created = string.Empty;
            public string amount = string.Empty;
            public string type = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

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

        private string profileImage = string.Empty;
        public string ProfileImage
        {
            get
            {
                return profileImage;
            }
            set
            {
                profileImage = value;
                OnPropertyChanged("ProfileImage");
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string note = string.Empty;
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        private double amount;
        public double Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private DateTime date;
        public DateTime Date
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

        private bool isCredited;
        public bool IsCredited
        {
            get
            {
                return isCredited;
            }
            set
            {
                isCredited = value;
                OnPropertyChanged("IsCredited");
            }
        }
    }
}
