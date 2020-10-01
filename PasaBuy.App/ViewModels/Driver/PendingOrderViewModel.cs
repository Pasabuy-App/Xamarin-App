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
                Order.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "received", "", "", "", "", (bool success, string data) => 
                {
                    if (success)
                    {
                        if (HtmlUtils.ConvertToPlainText(data) == "No order found")
                        {
                            TransactListData datas = JsonConvert.DeserializeObject<TransactListData>(data);

                            if(datas.data.Length != 0)
                            {
                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    string ItemID = datas.data[i].item_id;
                                    string Price = datas.data[i].price;
                                    string Product = datas.data[i].product;
                                    string Quantity = datas.data[i].quantity;
                                    string Status = datas.data[i].status;
                                    string Store = datas.data[i].store;
                                    string Date_created = datas.data[i].date_created;
                                    string Date_ordered = datas.data[i].date_ordered;
                                    string product_name = string.Empty;
                                    // TindaPress.Product.Instance.List((PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, );
                                    orderlist.Add(new TransactListData()
                                    {
                                        ItemID = ItemID,
                                        Price = Price,
                                        Product = Product,
                                        Quantity = Quantity,
                                        Status = Status,
                                        Store = Store,
                                        Date_created = Date_created,
                                        Date_ordered = Date_ordered

                                    });
                                }
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
