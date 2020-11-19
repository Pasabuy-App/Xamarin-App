

using FFImageLoading.Transformations;
using FFImageLoading.Work;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class OrderStatusViewModel : BaseViewModel
    {
        public bool IsCurrentStatus { get; set; }
        public static double _fee;
        public static double _total;
        public static double _subtotal;

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

        public string _stopWatch;
        public string stopWatch
        {
            get
            {
                return _stopWatch;
            }
            set
            {
                if (_timeStatus != value)
                {
                    _stopWatch = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public string _timeStatus;
        public string timeStatus
        {
            get
            {
                return _timeStatus;
            }
            set
            {
                if (_timeStatus != value)
                {
                    _timeStatus = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public StepStatus _isMoverFound;
        public StepStatus isMoverFound
        {
            get
            {
                return _isMoverFound;
            }
            set
            {
                if (_isMoverFound != value)
                {
                    _isMoverFound = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public StepStatus _isMoverOngoing;
        public StepStatus isMoverOngoing
        {
            get
            {
                return _isMoverOngoing;
            }
            set
            {
                if (_isMoverOngoing != value)
                {
                    _isMoverOngoing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public StepStatus _isOrderPreparing;
        public StepStatus isOrderPreparing
        {
            get
            {
                return _isOrderPreparing;
            }
            set
            {
                if (_isOrderPreparing != value)
                {
                    _isOrderPreparing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public StepStatus _isOrderShiping;
        public StepStatus isOrderShiping
        {
            get
            {
                return _isOrderShiping;
            }
            set
            {
                if (_isOrderShiping != value)
                {
                    _isOrderShiping = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public StepStatus _isOrderCompleted;
        public StepStatus isOrderCompleted
        {
            get
            {
                return _isOrderCompleted;
            }
            set
            {
                if (_isOrderCompleted != value)
                {
                    _isOrderCompleted = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool _isStore;
        public bool isStore
        {
            get
            {
                return _isStore;
            }
            set
            {
                if (_isStore != value)
                {
                    _isStore = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool _isMover;
        public bool isMover
        {
            get
            {
                return _isMover;
            }
            set
            {
                if (_isMover != value)
                {
                    _isMover = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public static string mover_id;

        public static string store_id;

        public static string order_id;

        public List<ITransformation> TurnGreyScale { get; set; }
        public bool isRunning = false;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                this.NotifyPropertyChanged();
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
        public System.Windows.Input.ICommand RefreshCommand { protected set; get; }
        //public int TimeLimit = 125;
        public static bool isTimer;
        public static bool myTimer;
        public OrderStatusViewModel()
        {
            this.Fee = _fee;
            this.Total = _total;
            this.SubTotal = _subtotal;
            TurnGreyScale = new List<ITransformation>()
            {
                new CustomTransformationSelector(),
            };

            this.timeStatus = "Estimated Time of Accepting by Store. If not, your order will be automatically cancelled.";

            RefreshCommand = new Xamarin.Forms.Command<string>((key) =>
            {
                CheckingOrder(order_id);
                IsRefreshing = false;
            });

            isTimer = true;
            myTimer = true;
            //CheckingOrder2(order_id);
            RefreshTimer();
            //OrderTimer();
            this.StoreMessage = new Xamarin.Forms.Command<object>(StoreMessageClicked);
            this.MoverMessage = new Xamarin.Forms.Command<object>(MoverMessageClicked);
        }
        public string store_name;
        public string mover_name;
        public string store_logo;
        public string mover_avatar;
        public string order_status;
        public int timer = 1800;
        public Xamarin.Forms.Command<object> StoreMessage { get; set; }
        private async void StoreMessageClicked(object obj)
        {
            if (!string.IsNullOrEmpty(store_id))
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    ViewModels.Chat.ChatMessageViewModel.odid = order_id;
                    ViewModels.Chat.ChatMessageViewModel.type = "store";
                    ViewModels.Chat.ChatMessageViewModel.user_id = store_id;
                    ViewModels.Chat.ChatMessageViewModel.ProfileNames = store_name;
                    ViewModels.Chat.ChatMessageViewModel.ProfileImages = store_logo;
                    await (App.Current.MainPage).Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new Views.Chat.ChatMessagePage()));
                    IsRunning = false;
                }
            }
        }
        public Xamarin.Forms.Command<object> MoverMessage { get; set; }
        private async void MoverMessageClicked(object obj)
        {
            if (!string.IsNullOrEmpty(mover_id))
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    ViewModels.Chat.ChatMessageViewModel.odid = order_id;
                    ViewModels.Chat.ChatMessageViewModel.type = "mover";
                    ViewModels.Chat.ChatMessageViewModel.user_id = mover_id;
                    ViewModels.Chat.ChatMessageViewModel.ProfileNames = mover_name;
                    ViewModels.Chat.ChatMessageViewModel.ProfileImages = mover_avatar;
                    await (App.Current.MainPage).Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new Views.Chat.ChatMessagePage()));
                    IsRunning = false;
                }
            }
        }

        public void RefreshTimer()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (myTimer)
                {
                    CheckingOrder2(order_id);
                }
                timer--;
                if (timer != 0 && timer > 0)
                {
                    int min = timer / 60;
                    int sec = timer % 60;
                    string label = min > 1 ? " minutes" : min == 1 ? " minute" : " seconds";
                    this.stopWatch = min + ":" + (sec >= 10 ? sec.ToString() : "0" + sec) + label;
                }
                return true;
            });
        }
        //Stopwatch stopwatch = new Stopwatch();
        public void OrderTimer()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (isTimer)
                {
                    //CheckingOrder2(order_id);
                    /*timer--;
                    if (timer != 0 && timer > 0)
                    {
                        int min = timer / 60;
                        int sec = timer % 60;
                        string label = min > 1 ? " minutes" : min == 1 ? " minute" : " seconds";
                        this.stopWatch = min + ":" + (sec >= 10 ? sec.ToString() : "0" + sec) + label;
                    }
                    /*else
                    {
                        this.timeStatus = "Thank you.";
                        //flag = false;
                        return false;
                    }*/
                }
                return true;
            });
        }
        public async void Popup()
        {
            //OrderTimer(false, 0);
            myTimer = false;
            this.isOrderCompleted = StepStatus.Completed;
            eCommerce.TransactionHistoryViewModel.LoadOrder();
            App.Current.MainPage.Navigation.PopModalAsync();
            await PopupNavigation.PushAsync(new PopupRateDriver());
        }
        public void CheckingOrder2(string odid)
        {
            try
            {
                Http.MobilePOS.Order.Instance.Listing("", odid, "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.OrderModel order = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);
                        for (int i = 0; i < order.data.Length; i++)
                        {
                            string stages = order.data[i].stages;

                            if (order_status != stages)
                            {
                                order_status = stages;
                                timer = 1800;
                            }

                            if (stages == "Accepted")
                            {
                                string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                store_id = order.data[i].stid;
                                store_name = order.data[i].store_name;
                                store_logo = logos;
                                AcceptedByStore();
                            }
                            if (stages == "Ongoing")
                            {
                                string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                store_id = order.data[i].stid;
                                store_name = order.data[i].store_name;
                                store_logo = logos;
                                mover_id = order.data[i].mover_id;
                                mover_name = order.data[i].driver_name;
                                mover_avatar = order.data[i].driver_avatar;
                                MoverFound();
                            }
                            if (stages == "Preparing")
                            {
                                string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                store_id = order.data[i].stid;
                                store_name = order.data[i].store_name;
                                store_logo = logos;
                                mover_id = order.data[i].mover_id;
                                mover_name = order.data[i].driver_name;
                                mover_avatar = order.data[i].driver_avatar;
                                StorePreparing();
                            }
                            if (stages == "Shipping")
                            {
                                string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                store_id = order.data[i].stid;
                                store_name = order.data[i].store_name;
                                store_logo = logos;
                                mover_id = order.data[i].mover_id;
                                mover_name = order.data[i].driver_name;
                                mover_avatar = order.data[i].driver_avatar;
                                OrderShipping();
                            }
                            if (stages == "Completed" || stages == "Cancelled")
                            {
                                string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                store_id = order.data[i].stid;
                                store_name = order.data[i].store_name;
                                store_logo = logos;
                                mover_id = order.data[i].mover_id;
                                mover_name = order.data[i].driver_name;
                                mover_avatar = order.data[i].driver_avatar;
                                OrderCompleted(order.data[i].driver_name, order.data[i].driver_avatar);
                                if (stages == "Completed")
                                {
                                    Popup();
                                }
                                else
                                {
                                    myTimer = false;
                                    eCommerce.TransactionHistoryViewModel.LoadOrder();
                                    App.Current.MainPage.Navigation.PopModalAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1OSVM.", "OK");
            }
        }
        public void CheckingOrder(string odid)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Order.Instance.Listing("", odid, "", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.OrderModel order = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                            for (int i = 0; i < order.data.Length; i++)
                            {
                                string stages = order.data[i].stages;
                                if (stages == "Accepted")
                                {
                                    string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                    store_id = order.data[i].stid;
                                    store_name = order.data[i].store_name;
                                    store_logo = logos;
                                    AcceptedByStore();
                                }
                                if (stages == "Ongoing")
                                {
                                    string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                    store_id = order.data[i].stid;
                                    store_name = order.data[i].store_name;
                                    store_logo = logos;
                                    mover_id = order.data[i].mover_id;
                                    mover_name = order.data[i].driver_name;
                                    mover_avatar = order.data[i].driver_avatar;
                                    MoverFound();
                                }
                                if (stages == "Preparing")
                                {
                                    string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                    store_id = order.data[i].stid;
                                    store_name = order.data[i].store_name;
                                    store_logo = logos;
                                    mover_id = order.data[i].mover_id;
                                    mover_name = order.data[i].driver_name;
                                    mover_avatar = order.data[i].driver_avatar;
                                    StorePreparing();
                                }
                                if (stages == "Shipping")
                                {
                                    string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                    store_id = order.data[i].stid;
                                    store_name = order.data[i].store_name;
                                    store_logo = logos;
                                    mover_id = order.data[i].mover_id;
                                    mover_name = order.data[i].driver_name;
                                    mover_avatar = order.data[i].driver_avatar;
                                    OrderShipping();
                                }
                                if (stages == "Completed" || stages == "Cancelled")
                                {
                                    string logos = !string.IsNullOrEmpty(order.data[i].store_logo) ? order.data[i].store_logo : "";
                                    store_id = order.data[i].stid;
                                    store_name = order.data[i].store_name;
                                    store_logo = logos;
                                    mover_id = order.data[i].mover_id;
                                    mover_name = order.data[i].driver_name;
                                    mover_avatar = order.data[i].driver_avatar;
                                    OrderCompleted(order.data[i].driver_name, order.data[i].driver_avatar);
                                    if (stages == "Completed")
                                    {
                                        Popup();
                                    }
                                }
                                isTimer = true;
                                //OrderTimer(true, (order.data[i].expiry * 60));
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1OSVM.", "OK");
                IsRunning = false;
            }
        }

        public void MoverFound()
        {
            isMover = true;
            this.isMoverFound = StepStatus.InProgress;
            this.timeStatus = "Estimated Time of Arrival.";
        }
        public void AcceptedByStore()
        {
            isStore = true;
            this.timeStatus = "Estimated Time of Accepting by Mover. If not, your order will be automatically cancelled.";
        }
        public void OrderConfirm()
        {
            this.isOrderCompleted = StepStatus.Completed;
            this.timeStatus = "Confirm your order.";
        }
        public void OrderCompleted(string mover_name, string mover_avtar)
        {
            PopupRateDriver.order_id = order_id;
            PopupRateDriver.avatar = Local.PSAProc.GetUrl(mover_avtar);
            PopupRateDriver.mover_name = mover_name;
            PopupRateDriver.mover_id = mover_id;
            this.isOrderCompleted = StepStatus.InProgress;
            this.isOrderShiping = StepStatus.Completed;
        }
        public void OrderShipping()
        {
            this.isOrderShiping = StepStatus.InProgress;
            this.isOrderPreparing = StepStatus.Completed;
            this.timeStatus = "Estimated Time of Arrival.";
        }
        public void StorePreparing()
        {
            this.isOrderPreparing = StepStatus.InProgress;
            this.isMoverOngoing = StepStatus.Completed;
            this.timeStatus = "Estimated Time of Arrival.";
        }
        public void MoverOnGoing()
        {
            this.isMoverOngoing = StepStatus.InProgress;
            this.isMoverFound = StepStatus.Completed;
            this.timeStatus = "Estimated Time of Arrival.";
        }


        public class CustomTransformationSelector : ITransformation
        {
            readonly ITransformation PlaceholderTransformation = new CircleTransformation(5d, "#EEEEEE");
            readonly ITransformation ImageTransformation = new GrayscaleTransformation();

            public string Key
            {
                get
                {
                    return "CustomTransformationSelector";
                }
            }

            public IBitmap Transform(IBitmap sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
            {
                if (isPlaceholder)
                {
                    return PlaceholderTransformation.Transform(sourceBitmap, path, source, isPlaceholder, key);
                }

                return ImageTransformation.Transform(sourceBitmap, path, source, isPlaceholder, key);
            }
        }
    }
}
