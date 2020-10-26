using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class PaymentsViewModel : BaseViewModel
    {
        private ObservableCollection<StorePayment> _walletTransactions;

        public ObservableCollection<StorePayment> WalletTransactions
        {
            get
            {
                return _walletTransactions;
            }
            set
            {
                _walletTransactions = value;
                this.NotifyPropertyChanged();
            }
        }

        public ICommand SetupCommand
        {
            get
            {
                return new Command<string>((x) => SetupClicked(x));
            }
        }
        private async void SetupClicked(string id)
        {
            await PopupNavigation.Instance.PushAsync(new PopupSetupWallet());
        }

        public ICommand WithdrawCommand
        {
            get
            {
                return new Command<string>((x) => WithdrawClicked(x));
            }
        }
        private async void WithdrawClicked(string id)
        {
            await PopupNavigation.Instance.PushAsync(new PopupStoreWithdraw());
        }

        public PaymentsViewModel()
        {
            _walletTransactions = new ObservableCollection<StorePayment>();

            for (int i = 0; i < 5; i++)
            {
                _walletTransactions.Add(new StorePayment()
                {
                    ID = "0",
                    Avatar = "Avatar.png",
                    TransactionName = "Lorz Becislao",
                    TransactionInfo = "Wallet Recharge",
                    TransactionDate = "20 Oct 2020",
                    Amount = 150
                });
            }
        }



    }
}
