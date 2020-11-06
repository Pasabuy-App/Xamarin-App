using System.ComponentModel;

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
        public string ID;
        public string avatar;
        public string amount;
        public string name;
        public string info;
        public string date_created;

        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _Avatar = string.Empty;
        public string Avatar
        {
            get { return _Avatar; }
            set
            {
                _Avatar = value;
                OnPropertyChanged("Avatar");
            }
        }

        private string _TransactionName = string.Empty;
        public string TransactionName
        {
            get { return _TransactionName; }
            set
            {
                _TransactionName = value;
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

        private double _Amount = 0.00;
        public double Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
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
