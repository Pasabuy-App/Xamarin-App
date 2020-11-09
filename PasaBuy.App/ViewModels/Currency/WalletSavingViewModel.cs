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
    public class WalletSavingViewModel : BaseViewModel
    {
        #region Fields
        public static string name;

        public static string avatar;

        private int selectedIndex;

        private double totalBalance;

        private string[] days, weeks, months, xValues;

        private ObservableCollection<Model> weekListItems;

        private ObservableCollection<Model> monthListItems;

        private ObservableCollection<Model> yearListItems;

        private ObservableCollection<Model> listItems;

        public ObservableCollection<TransactionChartData> ChartData { get; set; }

        public ObservableCollection<Model> DataSource { get; set; }

        private Command<object> itemTappedCommand;

        private DelegateCommand _sendMoney;

        private DelegateCommand _confirmSendCommand;

        public static string currency_id;
        public static string wallet_id;

        public static ObservableCollection<WalletSavingsModel> _SavingsList;
        public ObservableCollection<WalletSavingsModel> SavingsList
        {
            get
            {
                return _SavingsList;
            }
            set
            {
                _SavingsList = value;
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
        public WalletSavingViewModel()
        {
            /*WeekData();
            MonthData();
            YearData();
            days = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            weeks = new string[] { "Week 1", "Week 2", "Week 3", "Week 4" };
            months = new string[] { "Jan", "Mar", "May", "Jul", "Sep", "Nov" };
            ChartData = new ObservableCollection<TransactionChartData>();
            DataSource = new ObservableCollection<Model>()
            {
                new Model(){ Duration = "Week" },
                new Model(){ Duration = "Month" },
                new Model(){ Duration = "Year" },
            };
            ListItems = WeekListItems;*/
            _SavingsList = new ObservableCollection<WalletSavingsModel>();
            _SavingsList.Clear();
            /*_SavingsList.Add(new WalletModel()
            {
                ID = "0",
                ProfileImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl),
                Name = "Amelia Coleman",
                Note = "Refund",
                Amount = 85,
                Date = new DateTime(2019, 1, 28),
                IsCredited = true
            });
            _SavingsList.Add(new WalletModel()
            {
                ID = "1",
                ProfileImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl),
                Name = "Nell Sanchez",
                Note = "Food Order Bill",
                Amount = 15.75,
                Date = new DateTime(2019, 1, 26),
                IsCredited = false
            });*/
            CreateWallet();
            _SavingsList.CollectionChanged += CollectionChanges;
            //LoadData("");
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
                        WalletSavingsModel wallet = JsonConvert.DeserializeObject<WalletSavingsModel>(data);
                        for (int i = 0; i < wallet.data.Length; i++)
                        {
                            //this.WalletID = wallet.data[i].public_key;
                            //curreny_id = wallet.data[i].currency_id;

                            _SavingsList.Add(new WalletSavingsModel()
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
                    Http.CoinPress.Wallet.Instance.Create("SVS", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            WalletSavingsModel wallet = JsonConvert.DeserializeObject<WalletSavingsModel>(data);
                            for (int i = 0; i < wallet.data.Length; i++)
                            {
                                this.WalletID = wallet.data[i].public_key;
                                currency_id = wallet.data[i].currency_id;
                                wallet_id = wallet.data[i].public_key;
                                SendWalletSavings.currency_id = currency_id;
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
                Http.CoinPress.Wallet.Instance.Balance("SVS", async (bool success, string data) =>
                {
                    if (success)
                    {
                        WalletSavingsModel wallet = JsonConvert.DeserializeObject<WalletSavingsModel>(data);
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
        /// Gets or sets the my wallet items collection in a week.
        /// </summary>
        public ObservableCollection<Model> WeekListItems
        {
            get
            {
                return this.weekListItems;
            }

            set
            {
                this.weekListItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the my wallet items collection in a month.
        /// </summary>
        public ObservableCollection<Model> MonthListItems
        {
            get
            {
                return this.monthListItems;
            }

            set
            {
                this.monthListItems = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the my wallet items collection in a year.
        /// </summary>
        public ObservableCollection<Model> YearListItems
        {
            get
            {
                return this.yearListItems;
            }

            set
            {
                this.yearListItems = value;
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
        /// Gets or sets the selected index of combobox.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                this.selectedIndex = value;
                UpdateListViewData();
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
        private void WeekData()
        {
            weekListItems = new ObservableCollection<Model>()
            {
                new Model()
                {
                    ProfileImage = "ProfileImage1.png",
                    Name = "Phillip Estrada",
                    Title = "Cash Back",
                    Amount = 50,
                    Date = new DateTime(2019, 1, 7),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 50,
                    Date = new DateTime(2019, 1, 7),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage="ProfileImage11.png",
                    Name = "Essie Hansen",
                    Title = "XXXXXXX6585",
                    Amount = 60,
                    Date = new DateTime(2019, 1, 6),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 40,
                    Date = new DateTime(2019, 1, 6),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage7.png",
                    Name = "Amelia Coleman",
                    Title = "Refund",
                    Amount = 40,
                    Date = new DateTime(2019, 1, 5),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Recharge",
                    Amount = 60,
                    Date = new DateTime(2019, 1, 5),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage2.png",
                    Name = "Alta Sims",
                    Title = "Cash Back",
                    Amount = 60,
                    Date = new DateTime(2019, 1, 4),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 40.25,
                    Date = new DateTime(2019, 1, 4),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage3.png",
                    Name = "Blake Moore",
                    Title = "XXXXXXX6585",
                    Amount = 45,
                    Date = new DateTime(2019, 1, 3),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 55,
                    Date = new DateTime(2019, 1, 3),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage4.png",
                    Name = "Chase Blair",
                    Title = "Refund",
                    Amount = 65,
                    Date = new DateTime(2019, 1, 2),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 35,
                    Date = new DateTime(2019, 1, 2),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage5.png",
                    Name = "Bernard Daniels",
                    Title = "Cash Back",
                    Amount = 35,
                    Date = new DateTime(2019, 1, 1),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 65,
                    Date = new DateTime(2019, 1, 1),
                    IsCredited = false
                }
            };
        }

        /// <summary>
        /// Month data collection.
        /// </summary>
        private void MonthData()
        {
            monthListItems = new ObservableCollection<Model>()
            {
                new Model()
                {
                    ProfileImage = "ProfileImage7.png",
                    Name = "Amelia Coleman",
                    Title = "Refund",
                    Amount = 85,
                    Date = new DateTime(2019, 1, 28),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 15.75,
                    Date = new DateTime(2019, 1, 26),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage6.png",
                    Name = "Arthur Kim",
                    Title = "XXXXXXX6585",
                    Amount = 65,
                    Date = new DateTime(2019, 1, 20),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Delivery Bill",
                    Amount = 35,
                    Date = new DateTime(2019, 1, 18),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage9.png",
                    Name = "Bettie Conlon",
                    Title = "Refund",
                    Amount = 40,
                    Date = new DateTime(2019, 1, 12),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food Order Bill",
                    Amount = 60,
                    Date = new DateTime(2019, 1, 10),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage11.png",
                    Name = "Essie Hansen",
                    Title = "Cashback",
                    Amount = 30,
                    Date = new DateTime(2019, 1, 6),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Food order Bill",
                    Amount = 70,
                    Date = new DateTime(2019, 1, 5),
                    IsCredited = false
                },
            };
        }

        /// <summary>
        /// Year data collection.
        /// </summary>
        private void YearData()
        {
            yearListItems = new ObservableCollection<Model>()
            {
                new Model()
                {
                    ProfileImage = "ProfileImage6.png",
                    Name = "Arthur Kim",
                    Title = "XXXXXXX6585",
                    Amount = 65,
                    Date = new DateTime(2019, 11, 24),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Delivery Bill",
                    Amount = 35,
                    Date = new DateTime(2019, 11, 2),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage7.png",
                    Name = "Amelia Coleman",
                    Title = "XXXXXXX6585",
                    Amount = 70,
                    Date = new DateTime(2019, 9, 21),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 30.50,
                    Date = new DateTime(2019, 9, 8),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage2.png",
                    Name = "Alta Sims",
                    Title = "XXXXXXX6585",
                    Amount = 50,
                    Date = new DateTime(2019, 7, 18),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 50,
                    Date = new DateTime(2019, 7, 12),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage3.png",
                    Name = "Blake Moore",
                    Title = "Refund",
                    Amount = 35,
                    Date = new DateTime(2019, 5, 21),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 65,
                    Date = new DateTime(2019, 5, 15),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage4.png",
                    Name = "Chase Blair",
                    Title = "XXXXXXX6585",
                    Amount = 20,
                    Date = new DateTime(2019, 3, 15),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 80,
                    Date = new DateTime(2019, 3, 5),
                    IsCredited = false
                },
                new Model()
                {
                    ProfileImage = "ProfileImage6.png",
                    Name = "Arthur Kim",
                    Title = "Cashback",
                    Amount = 85,
                    Date = new DateTime(2019, 1, 26),
                    IsCredited = true
                },
                new Model()
                {
                    ProfileImage = "ProfileImage8.png",
                    Name = "Nell Sanchez",
                    Title = "Credit Card Bill",
                    Amount = 15,
                    Date = new DateTime(2019, 1, 13),
                    IsCredited = false
                }
            };
        }

        /// <summary>
        /// Method for update the listview items.
        /// </summary>
        private void UpdateListViewData()
        {
            switch (SelectedIndex)
            {
                case 0:
                    ListItems = WeekListItems;
                    xValues = days;
                    break;
                case 1:
                    ListItems = MonthListItems;
                    xValues = weeks;
                    break;
                case 2:
                    ListItems = YearListItems;
                    xValues = months;
                    break;
                default:
                    break;
            }
            UpdateChartData();
        }

        /// <summary>
        /// Method for update the chart data.
        /// </summary>
        private void UpdateChartData()
        {
            ChartData.Clear();
            TotalBalance = 0;

            var incomeCollection = ListItems.Where(item => item.IsCredited).ToList();
            var expenseCollection = ListItems.Where(item => !item.IsCredited).ToList();

            for (int i = 0; i < incomeCollection.Count; i++)
            {
                TotalBalance += incomeCollection[i].Amount;
                TotalBalance -= expenseCollection[i].Amount;
                ChartData.Add(new TransactionChartData(xValues[i], incomeCollection[i].Amount, expenseCollection[i].Amount, 4));
            }
        }

        /// <summary>
        /// Invoked when an item is selected from the my wallet page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }

        private async void SendMoneyClicked(object obj)
        {
            //PopupNavigation.Instance.PushAsync(new PopupSendWalletSavings());
            //PopupSendWalletSavings.currency_id = currency_id;
            await App.Current.MainPage.Navigation.PushModalAsync(new SendWalletSavings());
        }

        private void ConfirmSendClicked(object obj)
        {
            /* try
             {
                 //Console.WriteLine("wallet id: " + PopupSendWalletSavings.walletid + " amount: " + PopupSendWalletSavings.amount + " currency: " + PopupSendWalletSavings.currency_id);
                 CoinPress.Wallet.Instance.Send(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PopupSendWalletSavings.walletid, PopupSendWalletSavings.amount, currency_id, PopupSendWalletSavings.notes, (bool success, string data) =>
                 {
                     if (success)
                     {
                         //Console.WriteLine("." +this.Amount + ". ." + PopupSendWalletSavings.amount + ".");
                         //double amount = (Convert.ToDouble(this.Amount) - Convert.ToDouble(PopupSendWalletSavings.amount));
                         //this.Amount = "0";// amount.ToString();
                         //new Alert("Send Money", "Send money successfully.", "OK"); // back to wallet page
                         //LoadBalance();
                         _SavingsList.Clear();
                         LoadData(currency_id, "");
                         WalletSaving.LastIndex = 11;
                         new Alert("Send Money", "Send money successfully.", "OK");
                         PopupNavigation.Instance.PopAsync();
                         Console.WriteLine("Count:" + _SavingsList.Count);
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
             }*/
        }
        #endregion
    }
}