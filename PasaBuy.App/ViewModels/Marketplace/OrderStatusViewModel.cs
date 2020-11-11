

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
        public int TimeLimit = 30;
        public OrderStatusViewModel()
        {
            this.Fee = _fee;
            this.Total = _total;
            this.SubTotal = _subtotal;
            TurnGreyScale = new List<ITransformation>()
            {
                new CustomTransformationSelector(),
            };
            CheckingOrder(order_id);
            OrderTimer(true);
            RefreshCommand = new Xamarin.Forms.Command<string>((key) =>
            {
                CheckingOrder(order_id);
                IsRefreshing = false;
            });
        }

        Stopwatch stopwatch = new Stopwatch();
        public void OrderTimer(Boolean flag)
        {
            if (flag)
            {
                stopwatch.Start();
                Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    int countdown = TimeLimit - stopwatch.Elapsed.Seconds;

                    this.stopWatch = countdown.ToString() + "seconds";

                    if (countdown == 1)
                    {
                        this.timeStatus = "Thank you.";
                        Popup();
                        flag = false;
                        return false;
                    }

                    return true;
                });
            }
            else
            {
                stopwatch.Stop();
            }
        }
        public async void Popup()
        {
            App.Current.MainPage.Navigation.PopModalAsync();
            await PopupNavigation.PushAsync(new PopupRateDriver());
        }

        public void CheckingOrder(string odid)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    this.timeStatus = "Estimated Time of Accepting by Store. If not, your order will be automatically cancelled.";
                    Http.MobilePOS.Order.Instance.Listing("", odid, "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.OrderModel order = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.OrderModel>(data);

                            for (int i = 0; i < order.data.Length; i++)
                            {
                                string stages = order.data[i].stages;
                                if (stages == "Accepted")
                                {
                                    AcceptedByStore(order.data[i].stid);
                                }
                                if (stages == "Ongoing")
                                {
                                    MoverFound(order.data[i].mvid);
                                }
                                if (stages == "Preparing")
                                {
                                    StorePreparing();
                                }
                                if (stages == "Shipping")
                                {
                                    OrderShipping();
                                }
                                if (stages == "Completed" || stages == "Cancelled")
                                {
                                    OrderCompleted();
                                }
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

        public void AcceptedByStore(string stid)
        {
            store_id = stid;
            isStore = true;
            this.timeStatus = "Estimated Time of Accepting by Mover. If not, your order will be automatically cancelled.";
        }
        public void OrderConfirm()
        {
            this.isOrderCompleted = StepStatus.Completed;
            this.timeStatus = "Confirm your order.";
        }
        public void OrderCompleted()
        {
            this.isOrderCompleted = StepStatus.InProgress;
            this.isOrderShiping = StepStatus.Completed;
        }
        public void OrderShipping()
        {
            this.isOrderShiping = StepStatus.InProgress;
            this.isOrderPreparing = StepStatus.Completed;
        }
        public void StorePreparing()
        {
            this.isOrderPreparing = StepStatus.InProgress;
            this.isMoverOngoing = StepStatus.Completed;
        }
        public void MoverOnGoing()
        {
            this.isMoverOngoing = StepStatus.InProgress;
            this.isMoverFound = StepStatus.Completed;
        }

        public void MoverFound(string moverid)
        {
            mover_id = moverid;
            isMover = true;
            this.isMoverFound = StepStatus.InProgress;
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
