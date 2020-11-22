using PasaBuy.App.Models.Currency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DriverWalletViewModel : BaseViewModel
    {
        public static ObservableCollection<WalletCreditsModel> _transactions;
        public ObservableCollection<WalletCreditsModel> Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                _transactions = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public DriverWalletViewModel()
        {
            _transactions = new ObservableCollection<WalletCreditsModel>();

            RefreshCommand = new Command<string>((key) =>
            {
                _transactions.Clear();
                LoadTransactions();
                IsRefreshing = false;
            });
        }

        public ICommand RefreshCommand { protected set; get; }

        public void LoadTransactions()
        {
            _transactions.Add(new WalletCreditsModel()
            {
                ProfileImage = "Avatar.png",
                Name = "Lorz Becislao",
                Amount = 120.25,
                Note = "Bayad utang",
                Date = new DateTime(2020, 11, 22)
            });
        }
    }
}
