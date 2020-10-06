using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using PasaBuy.App.Models.Driver;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using PasaBuy.App.Views.PopupModals;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactOrderList : ContentPage
    {
        public TransactOrderList()
        {
            InitializeComponent();
            this.BindingContext = new PendingOrderViewModel();
        }

       

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var btn = (Label)sender;
            var id = btn.ClassId;

             try
             {
                HatidPress.Deliveries.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", id, "", "", "pending", (bool success, string data) =>
                {
                    //new Alert("", data, "ok");
                    if (success)
                    {
                        TransactListData datas = JsonConvert.DeserializeObject<TransactListData>(data);
                        if (datas.data.Length != 0 )
                        {
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                string store = datas.data[i].store_name;
                                string costumer_address = datas.data[i].customer_address;
                                string store_address = datas.data[i].store_address;
                                string user_lat = datas.data[i].customer_lat;
                                string user_long = datas.data[i].customer_long;
                                string store_lat = datas.data[i].store_lat;
                                string store_long = datas.data[i].store_long;
                                string order_time = datas.data[i].date_ordered;
                                string product = datas.data[i].product_name;

                                Views.PopupModals.PopupAcceptOrder.item_id = id.ToString();
                                Views.PopupModals.PopupAcceptOrder.storeName = store;
                                Views.PopupModals.PopupAcceptOrder.waypointAddress = store_address;
                                Views.PopupModals.PopupAcceptOrder.destinationAddress = costumer_address;
                                Views.PopupModals.PopupAcceptOrder.orderTime = order_time;
                                Views.PopupModals.PopupAcceptOrder.orderName = product;
                            }
                            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
                        }
                        
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }

                });

            }
             catch (FeatureNotSupportedException fnsEx)
             {
                 // Handle not supported on device exception
                 Console.WriteLine("Handle not supported on device exception" + " " + fnsEx);
             }
             catch (FeatureNotEnabledException fneEx)
             {
                 // Handle not enabled on device exception
                 Console.WriteLine("Handle not enabled on device exception" + " " + fneEx);
             }
             catch (PermissionException pEx)
             {
                 // Handle permission exception
                 Console.WriteLine("Handle permission exception" + " " + pEx);
             }
             catch (Exception ex)
             {
                 // Unable to get location
                 Console.WriteLine("Unable to get location" + " " + ex);
             }
        }
    }
}