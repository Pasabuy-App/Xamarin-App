using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using MobilePOS;

namespace PasaBuy.App.ViewModels.Driver
{
    public class PendingOrderViewModel : BaseViewModel
    {
        private Command<object> acceptOrder;

        public static ObservableCollection<TransactListData> orderlist;

        public  ObservableCollection<TransactListData> OrderList
        {
            get { return orderlist; }
            set { orderlist = value; this.NotifyPropertyChanged(); }
        }

        public PendingOrderViewModel()
        {

            orderlist = new ObservableCollection<TransactListData>();
            orderlist.Clear();
           
            LoadOrder();

        }

        public static void LoadOrder()
        {
            try
            {
                HatidPress.Deliveries.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "car / sedan", "", "received", (bool success, string data) => 
                {
                    if (success)
                    {
                        if (HtmlUtils.ConvertToPlainText(data) != "No order found")
                        {
                            TransactListData datas = JsonConvert.DeserializeObject<TransactListData>(data);

                            if (datas.data.Length != 0)
                            {
                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    string Store_logo = datas.data[i].store_logo;
                                    string ID = datas.data[i].id;
                                    string ItemID = datas.data[i].hash_id;
                                    string Price = datas.data[i].price;
                                    string Product = datas.data[i].product_name;
                                    string Quantity = datas.data[i].quantity;
                                    string Status = datas.data[i].status;
                                    string Store = datas.data[i].store_name;
                                    string Date_created = datas.data[i].date_created;
                                    string Date_ordered = datas.data[i].date_ordered;
                                    string store_lat = datas.data[i].store_lat;
                                    string store_long = datas.data[i].store_long;
                                    string store_address = datas.data[i].store_address;
                                    string customer_lat = datas.data[i].customer_lat;
                                    string customer_long = datas.data[i].customer_long;
                                    string customer_address = datas.data[i].customer_address;

                         
                                    orderlist.Add(new TransactListData()
                                    {
                                        Store_logo = Store_logo,
                                        ID = ID,
                                        Hash_id = ItemID,
                                        Price = Price,
                                        Product = Product,
                                        Quantity = Quantity,
                                        Status = Status,
                                        Store = Store,

                                        Store_lat = store_lat,
                                        Store_long = store_long,
                                        Store_address = store_address,

                                        Customer_lat = customer_lat,
                                        Customer_long = customer_long,
                                        Customer_address = customer_address,

                                        Date_created = Date_created,
                                        Date_ordered = Date_ordered,

                                    });
                                }
                            }

                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20450.", "OK");
            }
        }

        public Command<object> AcceptOrder
        {
            get
            {
                return this.acceptOrder ?? (this.acceptOrder = new Command<object>(this.OpenMap));
            }
        }

        private void OpenMap(object selectedItem)
        {
            new Alert("sample","sam","ok");
        }

    }
}
