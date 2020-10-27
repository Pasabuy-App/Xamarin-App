using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class StoreWallet : INotifyPropertyChanged
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

        private string wallet_id = string.Empty;
        public string WalletID
        {
            get { return wallet_id; }
            set
            {
                wallet_id = value;
                OnPropertyChanged("WalletID");
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

        private string wallet_name = string.Empty;
        public string WalletName
        {
            get { return wallet_name; }
            set
            {
                wallet_name = value;
                OnPropertyChanged("WalletName");
            }
        }

    }
}
