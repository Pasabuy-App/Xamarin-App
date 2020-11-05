using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Currency;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class PaymentsViewModel : BaseViewModel
    {
        public static ObservableCollection<StorePayment> _walletTransactions;
        public ObservableCollection<WalletCreditsModel> _walletCredits;

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

        public ObservableCollection<WalletCreditsModel> WalletCreditsInformation
        {
            get
            {
                return _walletCredits;
            }
            set
            {
                _walletCredits = value;
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
            if (!isRunning)
            {
                isRunning = true;
                await Task.Delay(300);
                SearchWalletPersonnel.key = this.AccountID;
                await App.Current.MainPage.Navigation.PushModalAsync(new SearchWalletPersonnel());
                isRunning = false;
            }

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
            if (!isRunning)
            {
                isRunning = true;
                await Task.Delay(300);
                await PopupNavigation.Instance.PushAsync(new PopupStoreWithdraw());
                isRunning = false;
            }
        }
        public bool _isRunning = false;
        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.NotifyPropertyChanged();
            }
        }
        public double _Balance;
        public double Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                _Balance = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _AccountName = string.Empty;
        public string AccountName
        {
            get
            {
                return _AccountName;
            }
            set
            {
                _AccountName = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _AccountID = string.Empty;
        public string AccountID
        {
            get
            {
                return _AccountID;
            }
            set
            {
                _AccountID = value;
                this.NotifyPropertyChanged();
            }
        }

        public PaymentsViewModel()
        {
            _walletTransactions = new ObservableCollection<StorePayment>();
            //_walletCredits = new ObservableCollection<WalletCreditsModel>();
            LoadInfo();
            _walletTransactions.CollectionChanged += CollectionChanges;
        }

        private void CollectionChanges(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                LoadInfo();
            }
        }

        public void LoadInfo()
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    Http.MobilePOS.Wallet.Instance.Info((bool success, string data) =>
                    {
                        if (success)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            Models.POSFeature.WalletModel wallet = JsonConvert.DeserializeObject<Models.POSFeature.WalletModel>(data);
                            for (int i = 0; i < wallet.data.Length; i++)
                            {
                                this.AccountID = wallet.data[i].pubkey;
                                this.AccountName = wallet.data[i].assigned_by;
                                this.Balance = Convert.ToDouble(wallet.data[i].balance);
                                for (int ii = 0; i < wallet.data[i].transactions.Count; ii++)
                                {
                                    if (wallet.data[i].transactions.Count == ii)
                                    {
                                        break;
                                    }
                                    DateTime dates = DateTime.ParseExact(wallet.data[i].transactions[ii].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                    _walletTransactions.Add(new StorePayment()
                                    {
                                        Id = wallet.data[i].transactions[ii].ID,
                                        Avatar = PSAProc.GetUrl(wallet.data[i].transactions[ii].avatar),
                                        TransactionName = wallet.data[i].transactions[ii].name,
                                        TransactionInfo = "Payment",
                                        TransactionDate = dates.ToString("dd MMM. yyyy"),
                                        Amount = Convert.ToDouble(wallet.data[i].transactions[ii].amount)
                                    });
                                }
                                isRunning = false;
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            isRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2WLT-I1PVM.", "OK");
                isRunning = false;
            }
        }
    }
}
