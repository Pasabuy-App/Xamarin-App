using PasaBuy.App.Models.Driver;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Settings
{
    public class MyTransactionDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<TransactListData> _transactionDetails;

        public ObservableCollection<TransactListData> TransactionDetails
        {
            get
            {
                return _transactionDetails;
            }
            set
            {
                _transactionDetails = value;
                this.NotifyPropertyChanged();
            }
        }

        public MyTransactionDetailsViewModel()
        {
            this.TransactionDetails = new ObservableCollection<TransactListData>()
            {
                new TransactListData
                {
                    ID = "1",
                    Product = "Cheeseburger without Cheese",
                    Quantity = "2",
                    Price = "120"
                },
                new TransactListData
                {
                    ID = "1",
                    Product = "Ice Cold Lemonade",
                    Quantity = "1",
                    Price = "110"
                },

            };
        }
    }
}
