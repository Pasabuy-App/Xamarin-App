﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<Models.POSFeature.OrderDetailsModel> productList;
        public ObservableCollection<Models.POSFeature.OrderDetailsModel> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                if (productList != value)
                {
                    productList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _Avatar;
        public string Avatar
        {
            get
            {
                return _Avatar;
            }
            set
            {
                if (_Avatar != value)
                {
                    _Avatar = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _remarks;
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                if (_remarks != value)
                {
                    _remarks = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _OrderID;
        public string OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                if (_OrderID != value)
                {
                    _OrderID = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _DateCreated;
        public string DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                if (_DateCreated != value)
                {
                    _DateCreated = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public double _TotalPrice;
        public double TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                if (_TotalPrice != value)
                {
                    _TotalPrice = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _Method;
        public string Method
        {
            get
            {
                return _Method;
            }
            set
            {
                if (_Method != value)
                {
                    _Method = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _Customer;
        public string Customer
        {
            get
            {
                return _Customer;
            }
            set
            {
                if (_Customer != value)
                {
                    _Customer = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool _isMessage = false;
        public bool isMessage
        {
            get
            {
                return _isMessage;
            }
            set
            {
                if (_isMessage != value)
                {
                    _isMessage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool _isDeclined = false;
        public bool isDeclined
        {
            get
            {
                return _isDeclined;
            }
            set
            {
                if (_isDeclined != value)
                {
                    _isDeclined = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool _isAccept = false;
        public bool isAccept
        {
            get
            {
                return _isAccept;
            }
            set
            {
                if (_isAccept != value)
                {
                    _isAccept = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _txtAcceptPreparingShipping;
        public string txtAcceptPreparingShipping
        {
            get
            {
                return _txtAcceptPreparingShipping;
            }
            set
            {
                if (_txtAcceptPreparingShipping != value)
                {
                    _txtAcceptPreparingShipping = value;
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

        public static string order_id;
        public static string avatar;
        public static string datecreated;
        public static double totalprice;
        public static string stages;
        public static string customer;
        public static string user_id;
        public static string method;
        public static string type = string.Empty;

        #endregion

        public OrderDetailsViewModel()
        {
            this.ProductList = new ObservableCollection<Models.POSFeature.OrderDetailsModel>();
            LoadOrder();
            this.Customer = customer;
            this.Avatar = avatar;
            this.OrderID = "Order ID: #" + order_id;
            this.DateCreated = datecreated;
            this.TotalPrice = totalprice;
            this.Method = method;

            if (type != "mover")
            {
                CheckPermission();
            }

            DeclinedCommand = new Command<object>(DeclinedClicked);
            AcceptedPreparingShippingCommand = new Command<object>(AcceptedPreparingShippingClicked);
            MessageCustomerCommand = new Command<object>(MessageCustomerClicked);
        }
        public void CheckPermission()
        {
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "decline_order")
                {
                    this.isDeclined = stages == "Pending" ? true : false;
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "accept_order")
                {
                    this.isAccept = stages == "Pending" || stages == "Preparing" || stages == "Ongoing" ? true : false;
                    this.txtAcceptPreparingShipping = stages != "Pending" ? stages != "Ongoing" ? "Ready for Shipping" : "Prepare Now" : "Accept";
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "prepare_order" || per.action == "ship_order")
                {
                    this.isAccept = stages == "Pending" || stages == "Preparing" || stages == "Ongoing" ? true : false;
                    this.txtAcceptPreparingShipping = stages != "Pending" ? stages != "Ongoing" ? "Ready for Shipping" : "Prepare Now" : "Accept";
                    break;
                }
            }

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "message_user")
                {
                    this.isMessage = stages == "Preparing" ? true : false;
                    break;
                }
            }
        }
        public Command<object> MessageCustomerCommand { get; set; }
        public Command<object> AcceptedPreparingShippingCommand { get; set; }
        public Command<object> DeclinedCommand { get; set; }

        private async void MessageCustomerClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                ViewModels.Chat.StoreConversationViewModel.odid = order_id;
                ViewModels.Chat.StoreConversationViewModel.user_id = user_id;
                ViewModels.Chat.StoreConversationViewModel.ProfileNames = customer;
                ViewModels.Chat.StoreConversationViewModel.ProfileImages = avatar;
                await(App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new Views.Chat.StoreConversationPage()));
                IsRunning = false;
            }
        }

        private void AcceptedPreparingShippingClicked(object obj)
        {
            string status = stages == "Pending" || stages == "Ongoing" ? "1" : "2";
            string stage = stages != "Pending" ? stages != "Preparing" ? "preparing" : "shipping" : "accepted";
            ProcessOrder(stage, status);
        }
        private void DeclinedClicked(object obj)
        {
            ProcessOrder("cancelled", "1");
        }

        public void LoadOrder()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Order.Instance.Listing(PSACache.Instance.UserInfo.stid, order_id, "", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.OrderModel product = JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                            for (int i = 0; i < 1; i++)
                            {
                                for (int ii = 0; ii < product.data[i].products.Count; ii++)
                                {
                                    double totalrpice = (Convert.ToDouble(product.data[i].products[ii].price) + product.data[i].products[ii].variants_price) * Convert.ToInt32(product.data[i].products[ii].quantity);
                                    this.ProductList.Add(new Models.POSFeature.OrderDetailsModel()
                                    {
                                        Price = totalrpice,
                                        Product = product.data[i].products[ii].product_name,
                                        Quantity = product.data[i].products[ii].quantity  + " " + " x ( " + product.data[i].products[ii].price + " + " + product.data[i].products[ii].variants_price + " )",
                                        Remarks = product.data[i].products[ii].remarks == "" ? "" : "Note: " + product.data[i].products[ii].remarks

                                    });
                                }
                                IsRunning = false;
                            }
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
                    new Controllers.Notice.Alert("Error Code: MPV2ODR-L1ODVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ODR-L1ODVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1ODVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ODR-L1ODVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

        public void ProcessOrder(string stage, string status)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Order.Instance.UpdateStages(order_id, stage, "", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            DashboardOrdersViewModel.stages = status;
                            DashboardOrdersViewModel.RefreshOrder(status);
                            await App.Current.MainPage.Navigation.PopModalAsync();

                            ViewModels.MobilePOS.DashboardOrdersViewModel._starttimer = true;
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
                    new Controllers.Notice.Alert("Error Code: MPV2ODR-U1ODVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ODR-U1ODVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-U1ODVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ODR-U1ODVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }
    }
}
