using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Position = Xamarin.Forms.Maps.Position;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using PasaBuy.App.Models.Driver;
using Xamarin.Essentials;

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
                MobilePOS.Order.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", "", (bool success, string data) =>
                {
                    //new Alert("", data, "ok");
                    if (success)
                    {
                        OrderListData datas = JsonConvert.DeserializeObject<OrderListData>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string costumer_address = datas.data[i].customer_address;
                            string store_address = datas.data[i].store_address;
                            double user_lat = datas.data[i].customer_lat;
                            double user_long = datas.data[i].customer_long;
                            double store_lat = datas.data[i].store_lat;
                            double store_long = datas.data[i].store_long;
                            Position s_p = new Position(store_lat, store_long);
                            Position c_p = new Position(user_lat, user_long);
                            //new Alert("ok", " " + store_address, "ok");
                            Views.Driver.Navigation.StoreAddress = store_address;
                            Views.Driver.Navigation.UserAddress = costumer_address;
                            Views.Driver.Navigation.StorePosition = s_p;
                            Views.Driver.Navigation.UserPosition = c_p;

                        }
                    }
                    else
                    {
                        new Alert("err", "err","ok");
                    }

                });

                Navigation.PushModalAsync(new Navigation());
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