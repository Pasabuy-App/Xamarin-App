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

        private string amount;
        private string profile_image = string.Empty;
        private string date = string.Empty;
        private string name = string.Empty;
        private string note = string.Empty;

        public string Amount
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
