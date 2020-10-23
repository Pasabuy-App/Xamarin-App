using MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.eCommerce;
using PasaBuy.App.Views.Settings;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.eCommerce
{
    /// <summary>
    /// ViewModel of transaction history template.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransactionHistoryViewModel : BaseViewModel
    {
        public static ObservableCollection<Transactions> transactionDetails;
        public ObservableCollection<Transactions> TransactionDetails
        {
            get { return transactionDetails; }
            set { transactionDetails = value; this.NotifyPropertyChanged(); }
        }
        bool isRunning = false;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    this.NotifyPropertyChanged();
                }
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
        public ICommand RefreshCommand { protected set; get; }
        #region Constructor
        public TransactionHistoryViewModel()
        {
            transactionDetails = new ObservableCollection<Transactions>();
            RefreshCommand = new Command<string>((key) =>
            {
                transactionDetails.Clear();
                LoadData("");
                IsRefreshing = false;
            });
            LoadData("");
            IsRunning = false;
        }
        public void LoadData(string offset)
        {
            try
            {
                IsRunning = true;
                Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Transactions order = JsonConvert.DeserializeObject<Transactions>(data);
                        if (order.data.Length != 0)
                        {
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                DateTime mydate = DateTime.ParseExact(order.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                string _status = order.data[i].stage != "cancelled" ? order.data[i].stage == "completed" ? "Delivered" : "On-Going" : "Cancelled";
                                transactionDetails.Add(new Transactions()
                                {
                                    ID = order.data[i].odid,
                                    CustomerName = order.data[i].store_name,
                                    TransactionDescription = "Order ID " + order.data[i].odid.GetHashCode().ToString(),
                                    Image = string.IsNullOrEmpty(order.data[i].store_logo) || order.data[i].store_logo == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(order.data[i].store_logo),
                                    TransactionAmount = "₱ " + Convert.ToDouble(order.data[i].totalprice).ToString(),
                                    Date = mydate.ToString("MMM. dd, yyyy hh:mm tt"),
                                    Status = _status
                                });
                            }
                            IsRunning = false;
                        }
                    }
                    else
                    {
                        IsRunning = false;
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                IsRunning = false;
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        public void SampleData()
        {
            /*var randomNum = new Random(0123456789);
            this.TransactionDetails = new ObservableCollection<Transactions>()
            {
                new Transactions
                {
                     CustomerName = "Lorz Dela Cruz",
                     TransactionDescription = "Cashback",
                     Image = PSAConfig.sfRootUrl + "ProfileImage15.png",
                     TransactionAmount = "+ $70",
                     Date = DateTime.Now.AddDays(randomNum.Next(-1000, 0)),
                     IsCredited = true
                },
                new Transactions
                {
                     CustomerName = "Miguel San Miguel",
                     TransactionDescription = "XXXXXXX6585",
                     Image = PSAConfig.sfRootUrl + "ProfileImage10.png",
                     TransactionAmount = "+ $80",
                     Date = DateTime.Now.AddDays(randomNum.Next(-1000, 0)),
                     IsCredited = true
                },
                new Transactions
                {
                     CustomerName = "Russel Desiguel",
                     TransactionDescription = "Recharge",
                     Image = PSAConfig.sfRootUrl + "ProfileImage11.png",
                     TransactionAmount = "- $50",
                     Date = DateTime.Now.AddDays(randomNum.Next(-1000, 0)),
                     IsCredited = false
                },
                new Transactions
                {
                     CustomerName = "Caezar Baltazar",
                     TransactionDescription = "Credit Card Bill",
                     Image = PSAConfig.sfRootUrl + "ProfileImage12.png",
                     TransactionAmount = "- $180",
                     Date = DateTime.Now.AddDays(randomNum.Next(-1000, 0)),
                     IsCredited = false
                },
            };*/
        }
        #endregion

        #region Properties


        #endregion

        #region Methods
        private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.ItemSelected));
            }
        }
        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private async void ItemSelected(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CanNavigate = false;
                var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Transactions;
                ViewModels.Settings.MyTransactionDetailsViewModel._id = item.ID;
                ViewModels.Settings.MyTransactionDetailsViewModel._image = item.Image;
                ViewModels.Settings.MyTransactionDetailsViewModel._name = item.CustomerName;
                ViewModels.Settings.MyTransactionDetailsViewModel._orderid = item.TransactionDescription;
                ViewModels.Settings.MyTransactionDetailsViewModel._storeaddress = "Store Address";
                ViewModels.Settings.MyTransactionDetailsViewModel._myadress = "My Address";
                ViewModels.Settings.MyTransactionDetailsViewModel._subtotal = item.TransactionAmount;
                ViewModels.Settings.MyTransactionDetailsViewModel._fee = "Free";
                ViewModels.Settings.MyTransactionDetailsViewModel._total = item.TransactionAmount;
                if (item.Status == "On-Going")
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new PasaBuy.App.Views.Marketplace.OrderStatusPage());
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new MyTransactionDetails());
                }
                IsRunning = false;
                CanNavigate = true;
            }
        }

        #endregion
    }
}
