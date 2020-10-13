using MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class MainViewModel : BaseViewModel
    {
        public static ObservableCollection<ChartData> _operationSummary;

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

        public ObservableCollection<ChartData> OperationSummary
        {
            get
            {
                return _operationSummary;
            }
            set
            {
                _operationSummary = value;
                this.NotifyPropertyChanged();
            }
        }


        public MainViewModel()
        {
            OperationSummary = new ObservableCollection<ChartData>();
            FillData();
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
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }

        }

        private void FillData()
        {
            ChartData obj = new ChartData()
            {
                Name = "Total",
                Value = "12000"
            };

            OperationSummary.Add(obj);

            ChartData obj1 = new ChartData()
            {
                Name = "Average",
                Value = "7000"
            };

            OperationSummary.Add(obj1);

            ChartData obj3 = new ChartData()
            {
                Name = "Total Orders",
                Value = "6000"
            };

            OperationSummary.Add(obj3);


        }
    }
}
