﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Settings;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.eCommerce
{
    /// <summary>
    /// ViewModel of transaction history template.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransactionHistoryViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.POSFeature.OrderModel> transactionDetails;
        public ObservableCollection<Models.POSFeature.OrderModel> TransactionDetails
        {
            get { return transactionDetails; }
            set { transactionDetails = value; this.NotifyPropertyChanged(); }
        }
        bool isRunning = false;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
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
        #region Constructor
        public TransactionHistoryViewModel()
        {
            transactionDetails = new ObservableCollection<Models.POSFeature.OrderModel>();
            RefreshCommand = new Command<string>((key) =>
            {
                transactionDetails.Clear();
                LoadData();
                IsRefreshing = false;
            });
            LoadData();
        }

        public static void LoadOrder()
        {
            try
            {
                transactionDetails.Clear();
                Http.MobilePOS.Order.Instance.Listing("", "", "", PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Models.POSFeature.OrderModel order = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                        bool isongoing = false;
                        bool iscompleted = false;
                        bool iscancelled = false;
                        for (int i = 0; i < order.data.Length; i++)
                        {
                            DateTime mydate = DateTime.ParseExact(order.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            string _status = order.data[i].stages != "Cancelled" ? order.data[i].stages == "Completed" ? "Delivered" : "On-Going" : "Cancelled";
                            if (_status == "Cancelled")
                            {
                                iscancelled = true;
                                iscompleted = false;
                                isongoing = false;
                            }
                            if (_status == "Delivered")
                            {
                                iscancelled = false;
                                iscompleted = true;
                                isongoing = false;
                            }
                            if (_status == "On-Going")
                            {
                                iscancelled = false;
                                iscompleted = false;
                                isongoing = true;
                            }
                            transactionDetails.Add(new Models.POSFeature.OrderModel()
                            {
                                ID = order.data[i].pubkey,
                                StoreName = order.data[i].store_name,
                                Pubkey = "Order ID: #" + order.data[i].pubkey,
                                StoreLogo = string.IsNullOrEmpty(order.data[i].store_logo) || order.data[i].store_logo == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(order.data[i].store_logo),
                                TotalPrice = Convert.ToDouble(order.data[i].total_price),
                                Total = Convert.ToDouble(order.data[i].total_price) + Convert.ToDouble(order.data[i].delivery_charges),
                                StoreAddress = order.data[i].store_address,
                                CustomerAddress = order.data[i].cutomer_address,
                                Method = order.data[i].method,
                                Stages = _status,
                                Delivery_Charges = order.data[i].delivery_charges,
                                DateCreated = mydate.ToString("MMM. dd, yyyy hh:mm tt"),
                                isOngoing = isongoing,
                                isCompleted = iscompleted,
                                isCancelled = iscancelled,
                            });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2ODR-L1THVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ODR-L1THVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1THVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ODR-L1THVM-" + err.ToString());
                }
            }
        }
        public void LoadData()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Order.Instance.Listing("", "", "", PSACache.Instance.UserInfo.wpid,(bool success, string data) =>
                    {
                        if (success)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            Models.POSFeature.OrderModel order = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                            bool isongoing = false;
                            bool iscompleted = false;
                            bool iscancelled = false;
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                DateTime mydate = DateTime.ParseExact(order.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                string _status = order.data[i].stages != "Cancelled" ? order.data[i].stages == "Completed" ? "Delivered" : "On-Going" : "Cancelled";
                                if (_status == "Cancelled")
                                {
                                    iscancelled = true;
                                    iscompleted = false;
                                    isongoing = false;
                                }
                                if (_status == "Delivered")
                                {
                                    iscancelled = false;
                                    iscompleted = true;
                                    isongoing = false;
                                }
                                if (_status == "On-Going")
                                {
                                    iscancelled = false;
                                    iscompleted = false;
                                    isongoing = true;
                                }
                                transactionDetails.Add(new Models.POSFeature.OrderModel()
                                {
                                    ID = order.data[i].pubkey,
                                    StoreName = order.data[i].store_name,
                                    Pubkey = "Order ID: #" + order.data[i].pubkey,
                                    StoreLogo = string.IsNullOrEmpty(order.data[i].store_logo) || order.data[i].store_logo == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(order.data[i].store_logo),
                                    TotalPrice = Convert.ToDouble(order.data[i].total_price),
                                    Total = Convert.ToDouble(order.data[i].total_price) + Convert.ToDouble(order.data[i].delivery_charges),
                                    StoreAddress = order.data[i].store_address,
                                    CustomerAddress = order.data[i].cutomer_address,
                                    Method = order.data[i].method,
                                    Stages = _status,
                                    Delivery_Charges = order.data[i].delivery_charges,
                                    DateCreated = mydate.ToString("MMM. dd, yyyy hh:mm tt"),
                                    isOngoing = isongoing,
                                    isCompleted = iscompleted,
                                    isCancelled = iscancelled,
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2ODR-L1THVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ODR-L1THVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1THVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ODR-L1THVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }
        #endregion

        #region Properties


        #endregion

        #region Methods
        private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.ItemSelected));
            }
        }
        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private async void ItemSelected(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Models.POSFeature.OrderModel;
                await Task.Delay(300);
                if (item.Stages == "On-Going")
                {
                    ViewModels.Marketplace.OrderStatusViewModel.order_id = item.ID;
                    ViewModels.Marketplace.OrderStatusViewModel._subtotal = item.TotalPrice;
                    ViewModels.Marketplace.OrderStatusViewModel._fee = Convert.ToDouble(item.Delivery_Charges);
                    ViewModels.Marketplace.OrderStatusViewModel._total = item.Total;
                    //ViewModels.Marketplace.OrderStatusViewModel.isTimer = true;
                    await App.Current.MainPage.Navigation.PushModalAsync(new PasaBuy.App.Views.Marketplace.OrderStatusPage());
                }
                else
                {
                    ViewModels.Settings.MyTransactionDetailsViewModel._id = item.ID;
                    ViewModels.Settings.MyTransactionDetailsViewModel._image = item.StoreLogo;
                    ViewModels.Settings.MyTransactionDetailsViewModel._name = item.StoreName;
                    ViewModels.Settings.MyTransactionDetailsViewModel._orderid = item.Pubkey;
                    ViewModels.Settings.MyTransactionDetailsViewModel._storeaddress = item.StoreAddress;
                    ViewModels.Settings.MyTransactionDetailsViewModel._myadress = item.CustomerAddress;
                    ViewModels.Settings.MyTransactionDetailsViewModel._subtotal = item.TotalPrice;
                    ViewModels.Settings.MyTransactionDetailsViewModel._fee = Convert.ToDouble(item.Delivery_Charges);
                    ViewModels.Settings.MyTransactionDetailsViewModel._total = item.Total;
                    ViewModels.Settings.MyTransactionDetailsViewModel._date_created = item.DateCreated;
                    ViewModels.Settings.MyTransactionDetailsViewModel._method = item.Method;
                    await App.Current.MainPage.Navigation.PushModalAsync(new MyTransactionDetails());
                }
                IsRunning = false;
            }
        }

        #endregion
    }
}
