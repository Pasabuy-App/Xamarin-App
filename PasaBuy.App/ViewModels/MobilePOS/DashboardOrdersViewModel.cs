using MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class DashboardOrdersViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<OrdersDataModel> orderList;
        public ObservableCollection<OrdersDataModel> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                this.NotifyPropertyChanged();
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
        public static string stages = "pending";
        #endregion
        public DashboardOrdersViewModel()
        {
            orderList = new ObservableCollection<OrdersDataModel>();
            //orderList.CollectionChanged += CollectionChanges;
            orderList.Clear();
            LoadOrder("pending");
            RefreshCommand = new Command<string>((key) =>
            {
                orderList.Clear();
                LoadRefresh(stages);
                IsRefreshing = false;
            });

        }
        /*private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                Views.StoreViews.TransactionDetailsView.stid = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).Store_ID;
                Views.StoreViews.TransactionDetailsView.user_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).User_ID;
                Views.StoreViews.TransactionDetailsView.avatar = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).Avatar;
                Views.StoreViews.TransactionDetailsView.id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).ID;
                Views.StoreViews.TransactionDetailsView.customer = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).Customer;
                Views.StoreViews.TransactionDetailsView.orderid = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).OrderID;
                Views.StoreViews.TransactionDetailsView.totalprice = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).TotalPrice;
                Views.StoreViews.TransactionDetailsView.datecreated = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).Date_Time;
                Views.StoreViews.TransactionDetailsView.method = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).Method;
                Views.StoreViews.TransactionDetailsView.order_type = stages != "pending" ? stages != "cancelled" ? stages != "shipping" ? "Received" : "Completed" : "Declined" : "Pending";
                Views.StoreViews.TransactionDetailsView.stage_type = stages;
                OrderDetailsViewModel.orderList.Clear();
                OrderDetailsViewModel.LoadOrder("", ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as OrdersDataModel).ID);
                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());
                await Task.Delay(300);
                IsRunning = false;
            }
        }*/

        /*private void CollectionChanges(object sender, EventArgs e)
        {
            orderList.Clear();
            LoadOrder(stages);
        }*/
        public void LoadRefresh(string stage)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, stage, PSACache.Instance.UserInfo.stid, "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            OrdersDataModel order = JsonConvert.DeserializeObject<OrdersDataModel>(data);
                            if (order.data.Length != 0)
                            {
                                for (int i = 0; i < order.data.Length; i++)
                                {
                                    orderList.Add(new OrdersDataModel()
                                    {
                                        ID = order.data[i].ID,
                                        User_ID = order.data[i].user_id,
                                        Store_ID = order.data[i].stid,
                                        Avatar = PSAProc.GetUrl("http://localhost/wordpress/wp-content/uploads/2020/10/28a921771e10207a2bc02e2647f345d0a8918b2e-IMG_20201015_083646_1.jpg"), //PSAProc.GetUrl(order.data[i].avatar),
                                        OrderID = "Order ID " + order.data[i].ID.GetHashCode().ToString(),
                                        Date_Time = order.data[i].date_created,
                                        TotalPrice = "₱ " + order.data[i].totalprice,
                                        Customer = order.data[i].customer,
                                        Method = order.data[i].method,
                                        Stage = order.data[i].stage,
                                        Product = order.data[i].product_name,
                                        Quantity = order.data[i].qty + " x " + order.data[i].price
                                    });
                                }
                                IsRunning = false;
                            }
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
        public static void LoadOrder(string stage)
        {
            try
            {
                Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, stage, PSACache.Instance.UserInfo.stid, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        OrdersDataModel order = JsonConvert.DeserializeObject<OrdersDataModel>(data);
                        if (order.data.Length != 0)
                        {
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                CultureInfo provider = new CultureInfo("fr-FR");
                                DateTime mydate = DateTime.ParseExact(order.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                orderList.Add(new OrdersDataModel()
                                {
                                    ID = order.data[i].ID,
                                    User_ID = order.data[i].user_id,
                                    Store_ID = order.data[i].stid,
                                    Avatar = PSAProc.GetUrl("http://localhost/wordpress/wp-content/uploads/2020/10/28a921771e10207a2bc02e2647f345d0a8918b2e-IMG_20201015_083646_1.jpg"), //PSAProc.GetUrl(order.data[i].avatar),
                                    OrderID = "Order ID " + order.data[i].ID.GetHashCode().ToString(),
                                    Date_Time = mydate.ToString("MMM. dd, yyyy hh:mm tt"),
                                    TotalPrice = "₱ " + order.data[i].totalprice,
                                    Customer = order.data[i].customer,
                                    Method = order.data[i].method,
                                    Stage = order.data[i].stage,
                                    Product = order.data[i].product_name,
                                    Quantity = order.data[i].qty + " x " + order.data[i].price
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
    }
}
