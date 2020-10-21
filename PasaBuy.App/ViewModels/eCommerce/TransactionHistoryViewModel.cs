using MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.eCommerce;
using PasaBuy.App.Views.Settings;
using System;
using System.Collections.ObjectModel;
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
        #region Constructor
        public TransactionHistoryViewModel()
        {
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

        public ICommand ItemSelectedCommand
        {
            get
            {
                return new Command<string>((x) => LoadDetails(x));
            }
        }

        public async void LoadDetails(string id)
        {

            if (!IsBusy)
            {
                IsBusy = true;
                CanNavigate = false;
                await App.Current.MainPage.Navigation.PushModalAsync(new MyTransactionDetails());
                await Task.Delay(300);
                IsBusy = false;
                CanNavigate = true;
            }
        }
  

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private void ItemSelected(object selectedItem)
        {
            //var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Transactions;
            //new Alert("2", "1: " + item.ID, "Ok");
            /*Views.StoreViews.TransactionDetailsView.id = item.ID;
            Views.StoreViews.TransactionDetailsView.customer = item.CustomerName;
            Views.StoreViews.TransactionDetailsView.orderid = item.ID;
            Views.StoreViews.TransactionDetailsView.totalprice = item.TransactionAmount;
            Views.StoreViews.TransactionDetailsView.datecreated = item.Date;
            Views.StoreViews.TransactionDetailsView.method = "Cash";// item.Method;
            Views.StoreViews.TransactionDetailsView.order_type = "completed";
            Views.StoreViews.TransactionDetailsView.stage_type = "completed";
            ViewModels.MobilePOS.OrderDetailsViewModel.LoadOrder("completed", "27");
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());*/
        }

        #endregion
    }
}
