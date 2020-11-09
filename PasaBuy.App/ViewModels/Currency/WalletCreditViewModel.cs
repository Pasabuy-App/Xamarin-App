using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Currency;
using PasaBuy.App.Views.Currency;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Model = PasaBuy.App.Models.Currency.Transaction;

namespace PasaBuy.App.ViewModels.Currency
{
    /// <summary>
    /// ViewModel for my wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class WalletCreditViewModel : BaseViewModel
    {
        #region Fields


        private double totalBalance;

        private ObservableCollection<Model> listItems;

        public ObservableCollection<TransactionChartData> ChartData { get; set; }

        public ObservableCollection<Model> DataSource { get; set; }

        private Command<object> itemTappedCommand;

        private DelegateCommand _sendMoney;

        private DelegateCommand _confirmSendCommand;

        public static string currency_id;
        public static string wallet_id;

        public static ObservableCollection<WalletCreditsModel> _CreditsList;
        public ObservableCollection<WalletCreditsModel> CreditsList
        {
            get
            {
                return _CreditsList;
            }
            set
            {
                _CreditsList = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool isRunning = false;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="WalletSavingViewModel" /> class.
        /// </summary>
        public WalletCreditViewModel()
        {
            _CreditsList = new ObservableCollection<WalletCreditsModel>();
            _CreditsList.Clear();
            CreateWallet();
            _CreditsList.CollectionChanged += CollectionChanges;

        }
        private void CollectionChanges(object sender, EventArgs e)
        {
            LoadBalance();
        }

        public static void LoadData(string currency, string offset)
        {
            try
            {
                Http.CoinPress.Wallet.Instance.Transactions("", "", currency, offset, (bool success, string data) =>
                {
                    if (success)
                    {
                        WalletCreditsModel wallet = JsonConvert.DeserializeObject<WalletCreditsModel>(data);
                        for (int i = 0; i < wallet.data.Length; i++)
                        {
                            _CreditsList.Add(new WalletCreditsModel()
                            {
                                ProfileImage = PSAProc.GetUrl(wallet.data[i].avatar),
                                Name = wallet.data[i].name,
                                Note = string.IsNullOrEmpty(wallet.data[i].remarks) || wallet.data[i].remarks == "None" ? "" : wallet.data[i].remarks,
                                Amount = Convert.ToDouble(wallet.data[i].amount),
                                Date = new DateTime(Convert.ToInt32(wallet.data[i].date_created.Substring(0, 4)), Convert.ToInt32(wallet.data[i].date_created.Substring(5, 2)), Convert.ToInt32(wallet.data[i].date_created.Substring(8, 2))),
                                IsCredited = wallet.data[i].type == "sender" ? false : true
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public void CreateWallet()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.CoinPress.Wallet.Instance.Create("CDT", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            WalletCreditsModel wallet = JsonConvert.DeserializeObject<WalletCreditsModel>(data);
                            for (int i = 0; i < wallet.data.Length; i++)
                            {
                                this.WalletID = wallet.data[i].public_key;
                                currency_id = wallet.data[i].currency_id;
                                wallet_id = wallet.data[i].public_key;
                                SendWalletCredits.currency_id = currency_id;
                                LoadBalance();
                                await Task.Delay(500);
                                LoadData(wallet.data[i].currency_id, "");
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsRunning = false;
            }
        }

        public void LoadBalance()
        {
            try
            {
                Http.CoinPress.Wallet.Instance.Balance("CDT", async (bool success, string data) =>
                {
                    if (success)
                    {
                        WalletCreditsModel wallet = JsonConvert.DeserializeObject<WalletCreditsModel>(data);
                        this.Amount = wallet.balance;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the my wallet items collection in a week.
        /// </summary>
        /// 
        public DelegateCommand SendMoney =>
          _sendMoney ?? (_sendMoney = new DelegateCommand(SendMoneyClicked));

        public DelegateCommand ConfirmSendCommand =>
           _confirmSendCommand ?? (_confirmSendCommand = new DelegateCommand(ConfirmSendClicked));

        public string _Avatar;
        public string Avatar
        {
            get
            {
                return this._Avatar;
            }

            set
            {
                this._Avatar = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _Message;
        public string Message
        {
            get
            {
                return this._Message;
            }

            set
            {
                this._Message = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _Recipient;
        public string Recipient
        {
            get
            {
                return this._Recipient;
            }

            set
            {
                this._Recipient = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _Amount;
        public string Amount
        {
            get
            {
                return this._Amount;
            }

            set
            {
                this._Amount = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _WalletID;
        public string WalletID
        {
            get
            {
                return this._WalletID;
            }

            set
            {
                this._WalletID = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the my wallet items collection.
        /// </summary>
        public ObservableCollection<Model> ListItems
        {
            get
            {
                return this.listItems;
            }

            set
            {
                this.listItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        /// <summary>
        /// Gets or sets the total balance remaining in the wallet.
        /// </summary>
        public double TotalBalance
        {
            get
            {
                return totalBalance;
            }
            set
            {
                this.totalBalance = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Week data collection.
        /// </summary>
        /// 
        private async void SendMoneyClicked(object obj)
        {
            if (!IsRunning) 
            { 
                IsRunning = true;
                //await PopupNavigation.Instance.PushAsync(new PopupSendWalletCredit());
                Views.Currency.SendWalletSavings.currency_id = currency_id;
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Currency.SendWalletSavings());
                IsRunning = false;
            }
        }

        private void ConfirmSendClicked(object obj)
        {
            new Alert("Ok", "Do something", "Ok");
        }

        /// <summary>
        /// Invoked when an item is selected from the my wallet page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }

        #endregion
    }
}