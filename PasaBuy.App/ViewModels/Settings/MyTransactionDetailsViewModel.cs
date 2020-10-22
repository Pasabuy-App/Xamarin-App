using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MobilePOS;
using System.Globalization;
using Newtonsoft.Json;
using PasaBuy.App.Models.eCommerce;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.ViewModels.Settings
{
    public class MyTransactionDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<TransactListData> _transactionDetails;
        public ObservableCollection<TransactListData> TransactionDetails
        {
            get
            {
                return _transactionDetails;
            }
            set
            {
                _transactionDetails = value;
                this.NotifyPropertyChanged();
            }
        }

        public string image;
        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.NotifyPropertyChanged();
            }
        }

        public string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                this.NotifyPropertyChanged();
            }
        }

        public string orderID;
        public string OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
                this.NotifyPropertyChanged();
            }
        }

        public string storeAddress;
        public string StoreAddress
        {
            get
            {
                return storeAddress;
            }
            set
            {
                storeAddress = value;
                this.NotifyPropertyChanged();
            }
        }

        public string subTotal;
        public string SubTotal
        {
            get
            {
                return subTotal;
            }
            set
            {
                subTotal = value;
                this.NotifyPropertyChanged();
            }
        }

        public string fee;
        public string Fee
        {
            get
            {
                return fee;
            }
            set
            {
                fee = value;
                this.NotifyPropertyChanged();
            }
        }

        public string total;
        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                this.NotifyPropertyChanged();
            }
        }

        public string myAddress;
        public string MyAddress
        {
            get
            {
                return myAddress;
            }
            set
            {
                myAddress = value;
                this.NotifyPropertyChanged();
            }
        }
        public static string _id;
        public static string _image;
        public static string _name;
        public static string _orderid;
        public static string _storeaddress;
        public static string _myadress;
        public static string _subtotal;
        public static string _fee;
        public static string _total;
        public MyTransactionDetailsViewModel()
        {
            this.Image = _image;
            this.Name = _name;
            this.OrderID = _orderid;
            this.StoreAddress = _storeaddress;
            this.MyAddress = _myadress;
            this.OrderID = _orderid;
            this.Fee = _fee;
            this.Total = _total;
            this.SubTotal = _subtotal;
            _transactionDetails = new ObservableCollection<TransactListData>();
            LoadData(_id);
        }
        public void LoadData(string odid)
        {
            try
            {
                Customers.Instance.OrderList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", odid, (bool success, string data) =>
                {
                    if (success)
                    {
                        TransactListData order = JsonConvert.DeserializeObject<TransactListData>(data);
                        if (order.data.Length != 0)
                        {
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                _transactionDetails.Add(new TransactListData()
                                {
                                    Product = order.data[i].product_name,
                                    Quantity = order.data[i].quantity,
                                    Price =  Convert.ToDouble(order.data[i].price)
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
