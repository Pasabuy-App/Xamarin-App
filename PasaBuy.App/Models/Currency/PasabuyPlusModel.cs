using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Currency
{
    public class PasabuyPlusModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public PlusData[] data;
        public class PlusData
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

        public string balance;

        private double amount;
        private string profile_image = string.Empty;
        private DateTime date;
        private string name = string.Empty;
        private string note = string.Empty;
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
        public string ProfileImage
        {
            get
            {
                return profile_image;
            }
            set
            {
                profile_image = value;
                OnPropertyChanged("ProfileImage");
            }
        }

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


    }
}
