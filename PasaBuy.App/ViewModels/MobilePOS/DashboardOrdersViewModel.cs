using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class DashboardOrdersViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<Models.POSFeature.OrderModel> orderList;
        public ObservableCollection<Models.POSFeature.OrderModel> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                if (orderList != value)
                {
                    orderList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }

        public static string stages = "1";

        #endregion
        public DashboardOrdersViewModel()
        {
            orderList = new ObservableCollection<Models.POSFeature.OrderModel>();

            LoadOrder(stages);
            RefreshCommand = new Command<string>((key) =>
            {
                LoadOrder(stages);
                IsRefreshing = false;
            });
            _starttimer = true;
            StartTimer();
        }

        public static bool _starttimer;
        public void StartTimer()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (_starttimer)
                {
                    CheckPending();
                }
                return true;
            });
        }
        public void CheckPending()
        {
            try
            {
                Http.MobilePOS.Order.Instance.Listing(PSACache.Instance.UserInfo.stid, "", stages, "", async (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.OrderModel order = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);
                        if (order.data.Length > 0)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                if (order.data[i].stages == "Pending" || order.data[i].stages == "Ongoing")
                                {
                                    CultureInfo provider = new CultureInfo("fr-FR");
                                    string dates = string.IsNullOrEmpty(order.data[i].date_created) ? "0000-00-00 00:00:00" : order.data[i].date_created;
                                    DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);

                                    OrderDetailsViewModel.avatar = PSAProc.GetUrl(order.data[i].avatar);
                                    OrderDetailsViewModel.order_id = order.data[i].pubkey;
                                    OrderDetailsViewModel.datecreated = date.ToString("MMM. dd, yyyy hh:mm tt");
                                    OrderDetailsViewModel.totalprice = Convert.ToDouble(order.data[i].total_price);
                                    OrderDetailsViewModel.stages = order.data[i].stages;
                                    OrderDetailsViewModel.customer = order.data[i].customer;
                                    OrderDetailsViewModel.method = order.data[i].method;
                                    OrderDetailsViewModel.user_id = order.data[i].order_by;
                                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());
                                    _starttimer = false;
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1DOVM.", "OK");
            }
        }
        private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.ItemSelected));
            }
        }
        private async void ItemSelected(object selectedItem)
        {

            if (!IsRunning)
            {
                IsRunning = true;
                var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Models.POSFeature.OrderModel;
                OrderDetailsViewModel.avatar = item.Avatar;
                OrderDetailsViewModel.order_id = item.ID;
                OrderDetailsViewModel.datecreated = item.DateCreated;
                OrderDetailsViewModel.totalprice = item.TotalPrice;
                OrderDetailsViewModel.stages = item.Stages;
                OrderDetailsViewModel.customer = item.CustomerName;
                OrderDetailsViewModel.method = item.Method;
                OrderDetailsViewModel.user_id = item.Order_By;
                OrderDetailsViewModel.type = "store";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());
                _starttimer = false;
                IsRunning = false;
            }
        }

        public void LoadOrder(string stages)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    orderList.Clear();
                    Http.MobilePOS.Order.Instance.Listing(PSACache.Instance.UserInfo.stid, "", stages, "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.OrderModel order = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                CultureInfo provider = new CultureInfo("fr-FR");
                                string dates = string.IsNullOrEmpty(order.data[i].date_created) ? "0000-00-00 00:00:00" : order.data[i].date_created;
                                DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);
                                orderList.Add(new Models.POSFeature.OrderModel()
                                {
                                    Order_By = order.data[i].order_by,
                                    ID = order.data[i].pubkey,
                                    Pubkey = "Order ID: #" + order.data[i].pubkey,
                                    StoreName = order.data[i].store_name,
                                    DateCreated = date.ToString("MMM. dd, yyyy hh:mm tt"),
                                    TotalPrice = Convert.ToDouble(order.data[i].total_price),
                                    Method =  order.data[i].method,
                                    StoreAddress = order.data[i].store_address,
                                    CustomerAddress = order.data[i].cutomer_address,
                                    CustomerName = order.data[i].customer,
                                    Avatar = string.IsNullOrEmpty(PSAProc.GetUrl(order.data[i].avatar)) || PSAProc.GetUrl(order.data[i].avatar) == "None" ? "Avatar.png" : PSAProc.GetUrl(order.data[i].avatar),
                                    Stages = order.data[i].stages
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1DOVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshOrder(string stages)
        {
            try
            {
                orderList.Clear();
                Http.MobilePOS.Order.Instance.Listing(PSACache.Instance.UserInfo.stid, "", stages, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.OrderModel order = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);
                        for (int i = 0; i < order.data.Length; i++)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            string dates = string.IsNullOrEmpty(order.data[i].date_created) ? "0000-00-00 00:00:00" : order.data[i].date_created;
                            DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);
                            orderList.Add(new Models.POSFeature.OrderModel()
                            {
                                Order_By = order.data[i].order_by,
                                ID = order.data[i].pubkey,
                                Pubkey = "Order ID: #" + order.data[i].pubkey,
                                StoreName = order.data[i].store_name,
                                DateCreated = date.ToString("MMM. dd, yyyy hh:mm tt"),
                                TotalPrice = Convert.ToDouble(order.data[i].total_price),
                                Method = order.data[i].method,
                                StoreAddress = order.data[i].store_address,
                                CustomerAddress = order.data[i].cutomer_address,
                                CustomerName = order.data[i].customer,
                                Avatar = PSAProc.GetUrl(order.data[i].avatar),
                                Stages = order.data[i].stages
                            });
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L2DOVM.", "OK");
            }
        }
    }
}
