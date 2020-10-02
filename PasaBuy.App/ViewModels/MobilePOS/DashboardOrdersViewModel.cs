using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MobilePOS;
using Newtonsoft.Json;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class DashboardOrdersViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<OrdersDataModel> orderList;
        public ObservableCollection<OrdersDataModel> OrderList
        {
            get { return orderList; }
            set { orderList = value; this.NotifyPropertyChanged(); }
        }
        #endregion
        public DashboardOrdersViewModel()
        {
            orderList = new ObservableCollection<OrdersDataModel>();
            LoadOrder("pending", "");
        }
        public static void LoadOrder(string stage, string odid)
        {
            try
            {
                orderList.Clear();
                Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, stage, PSACache.Instance.UserInfo.stid, odid, (bool success, string data) =>
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
                                    ID = order.data[i].odid,
                                    OrderID = "Order ID " + order.data[i].odid.GetHashCode().ToString(),
                                    Date_Time = order.data[i].date_created,
                                    TotalPrice = "PHP " + order.data[i].totalprice,
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
                /*orderList.Add(new OrdersDataModel()
                {
                    ID = "1",
                    OrderID = "Order ID " + "15".GetHashCode().ToString(),
                    Date_Time = "Sept. 5 2020, 09:10 AM",
                    TotalPrice = "PHP 250",
                    Customer = "Malakas",
                    Method = "Cash"
                });*/
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }

        }
    }
}
