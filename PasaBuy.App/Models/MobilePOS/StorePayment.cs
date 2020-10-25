using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class StorePayment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string Id = string.Empty;
        public string ID
        {
            get { return Id; }
            set
            {
                Id = value;
                OnPropertyChanged("ID");
            }
        }

        private string avatar = string.Empty;
        public string Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                OnPropertyChanged("Avatar");
            }
        }

        private string transaction_name = string.Empty;
        public string TransactionName
        {
            get { return transaction_name; }
            set
            {
                transaction_name = value;
                OnPropertyChanged("TransactionName");
            }
        }

        private string transaction_info = string.Empty;
        public string TransactionInfo
        {
            get { return transaction_info; }
            set
            {
                transaction_info = value;
                OnPropertyChanged("TransactionInfo");
            }
        }

        private float amount;
        public float Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private string transaction_date = string.Empty;
        public string TransactionDate
        {
            get { return transaction_date; }
            set
            {
                transaction_date = value;
                OnPropertyChanged("TransactionDate");
            }
        }


    }
}
