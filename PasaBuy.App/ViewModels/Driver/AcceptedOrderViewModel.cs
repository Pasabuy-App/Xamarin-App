using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using PasaBuy.App.ViewModels.Driver;


namespace PasaBuy.App.ViewModels.Driver
{
    public class AcceptedOrderViewModel : BaseViewModel
    {
        private Command<object> acceptOrder;

        public static ObservableCollection<AcceptedListOrder> acceptedorderlist;

        public ObservableCollection<AcceptedListOrder> Acceptedorderlist
        {
            get { return acceptedorderlist; }
            set { acceptedorderlist = value; this.NotifyPropertyChanged(); }
        }


        public AcceptedOrderViewModel()
        {

            acceptedorderlist = new ObservableCollection<AcceptedListOrder>();
            acceptedorderlist.Clear();

            LoadOrder();
        }

        public static void LoadOrder()
        {
            try
            {
                HatidPress.Deliveries.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", PSACache.Instance.UserInfo.wpid, "", (bool success, string data) => 
                {
                    if (success)
                    {
                        AcceptedListOrder datas = JsonConvert.DeserializeObject<AcceptedListOrder>(data);
                        for(int i = 0; i < datas.data.Length; i++)
                        {
                            if (datas.data.Length != 0)
                            {
                                string id = datas.data[i].id;
                                string mover_id = string.Empty;
                                string order_id = datas.data[i].order_id;
                                string vechile_type = datas.data[i].vehicle_type;
                                string store_name = datas.data[i].store_name;

                                double waypoint_lat = string.IsNullOrEmpty(datas.data[i].waypoint_lat.ToString()) ? 0 : Convert.ToDouble(datas.data[i].waypoint_lat);
                                double waypoint_long = string.IsNullOrEmpty(datas.data[i].waypoint_long.ToString()) ? 0 : Convert.ToDouble(datas.data[i].waypoint_long);
                                double destination_lat = string.IsNullOrEmpty(datas.data[i].destination_lat.ToString()) ? 0 : Convert.ToDouble(datas.data[i].destination_lat);
                                double destination_long = string.IsNullOrEmpty(datas.data[i].destination_long.ToString()) ? 0 : Convert.ToDouble(datas.data[i].destination_long);
                                double package_distance = string.IsNullOrEmpty(datas.data[i].package_distance.ToString()) ? 0 : Convert.ToDouble(datas.data[i].package_distance);
                                double delivery_distance = string.IsNullOrEmpty(datas.data[i].delivery_distance.ToString()) ? 0 : Convert.ToDouble(datas.data[i].delivery_distance);

                                string destination_address = datas.data[i].destination_address;
                                string waypoint_address = datas.data[i].waypoint_address;
                                int fee = datas.data[i].fee;
                                string date_accepted = datas.data[i].date_accepted;
                                string date_created = datas.data[i].date_created;
                                acceptedorderlist.Add(new AcceptedListOrder()
                                {
                                    ID = id,
                                    
                                    WaypointAddress = waypoint_address,
                                    WaypointLat = waypoint_lat,
                                    WaypointLong = waypoint_long,

                                    DestinationAddress = destination_address,
                                    DestinationLat = destination_lat,
                                    DestinationLong = destination_long,
                                    PackageDistance = package_distance,
                                    DeliveryDistance = delivery_distance,
                                    Fee = fee,
                                    StoreName = store_name,

                                }) ;

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


    }
}
