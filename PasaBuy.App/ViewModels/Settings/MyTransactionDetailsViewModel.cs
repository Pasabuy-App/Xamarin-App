using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.Settings
{
    public class MyTransactionDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<Models.POSFeature.OrderDetailsModel> _transactionDetails;
        public ObservableCollection<Models.POSFeature.OrderDetailsModel> TransactionDetails
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

        public double subTotal;
        public double SubTotal
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

        public double fee;
        public double Fee
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

        public double total;
        public double Total
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
        public static double _subtotal;
        public static double _fee;
        public static double _total;
        public static string _method;
        public static string _date_created;
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

            _transactionDetails = new ObservableCollection<Models.POSFeature.OrderDetailsModel>();
            LoadData(_id);
        }
        public void LoadData(string odid)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Http.MobilePOS.Order.Instance.Listing("", odid, "", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.OrderModel product = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                            for (int i = 0; i < 1; i++)
                            {
                                for (int ii = 0; ii < product.data[i].products.Count; ii++)
                                {
                                    double totalrpice = (Convert.ToDouble(product.data[i].products[ii].price) + product.data[i].products[ii].variants_price) * Convert.ToInt32(product.data[i].products[ii].quantity);
                                    _transactionDetails.Add(new Models.POSFeature.OrderDetailsModel()
                                    {
                                        Price = totalrpice,
                                        Product = product.data[i].products[ii].product_name,
                                        Quantity = product.data[i].products[ii].quantity + " " + " x ( " + product.data[i].products[ii].price + " + " + product.data[i].products[ii].variants_price + " )"
                                    });
                                }
                            }
                            IsBusy = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsBusy = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1MTDVM.", "OK");
                IsBusy = false;
            }
        }
    }
}
