using System;
using System.Collections.ObjectModel;
using PasaBuy.App.Local;
using PasaBuy.App.Models.eCommerce;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;

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
        #region Constructor
        public TransactionHistoryViewModel()
        {
            this.ItemSelectedCommand = new Command(this.ItemSelected);
            transactionDetails = new ObservableCollection<Transactions>();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Transactions order = JsonConvert.DeserializeObject<Transactions>(data);
                        if (order.data.Length != 0)
                        {
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                transactionDetails.Add(new Transactions()
                                {
                                    ID = order.data[i].odid,
                                    CustomerName = order.data[i].store_name,
                                    TransactionDescription = "Order ID " + order.data[i].odid.GetHashCode().ToString(),
                                    Image = PSAProc.GetUrl(order.data[i].store_logo),
                                    TransactionAmount = "P " + order.data[i].totalprice,
                                    Date = order.data[i].date_created,
                                    IsCredited = false
                                });
                            }
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
        /// <summary>
        /// Gets or sets the employee details.
        /// </summary>
        /// <value>The employee details.</value>
        //public ObservableCollection<Transactions> TransactionDetails { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private void ItemSelected(object selectedItem)
        {
            // Do something
        }

        #endregion
    }
}
