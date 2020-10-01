using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
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
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDriverView : ContentPage
    {
        public TransactionDriverView()
        {
            InitializeComponent();
            this.BindingContext = new PendingOrderViewModel();

        }

        private void ShowAcceptOrder(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void NewOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var btn = (Label)sender;
            var id = btn.ClassId;

            try
            {
                MobilePOS.Order.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", id.ToString(), "", "", (bool success, string data) =>
                {
                    //new Alert("", data, "ok");
                    if (success)
                    {
                        OrderListData datas = JsonConvert.DeserializeObject<OrderListData>(data);
                        if (datas.data.Length != 0)
                        {
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                string store = datas.data[i].store;
                                string costumer_address = datas.data[i].customer_address;
                                string store_address = datas.data[i].store_address;
                                double user_lat = datas.data[i].customer_lat;
                                double user_long = datas.data[i].customer_long;
                                double store_lat = datas.data[i].store_lat;
                                double store_long = datas.data[i].store_long;
                                string order_time = datas.data[i].date_ordered;
                                string product = datas.data[i].product;

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