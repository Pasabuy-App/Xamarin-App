using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OperationsViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.POSFeature.OperationModel> _daysOfTheWeek;

        public static ObservableCollection<Models.POSFeature.OperationModel> _operationsList;

        public ObservableCollection<Models.POSFeature.OperationModel> DaysOfTheWeek
        {
            get
            {
                return _daysOfTheWeek;
            }
            set
            {
                _daysOfTheWeek = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Models.POSFeature.OperationModel> OperationsList
        {
            get
            {
                return _operationsList;
            }
            set
            {
                _operationsList = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool _IsRunning = false;
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

        public bool _GetStarted;
        public bool GetStarted
        {
            get
            {
                return _GetStarted;
            }
            set
            {
                _GetStarted = value;
                this.NotifyPropertyChanged();
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                return new Command<string>((x) => Submit());
            }
        }
        private async void Submit()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                GetStarted = false;
                Local.PSACache.Instance.UserInfo.store_operation = true;
                Local.PSACache.Instance.SaveUserData();
                await System.Threading.Tasks.Task.Delay(300);
                IsRunning = false;
            }
        }
        public Command<object> ViewOperationCommand { get; set; }
        private async void ViewClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;

                var operation = obj as Models.POSFeature.OperationModel;
                PopupViewOperations.opid = operation.Id;
                PopupViewOperations.open = operation.Opening;
                PopupViewOperations.close = operation.Closing;
                PopupViewOperations.date = operation.FullSchedule;
                await PopupNavigation.Instance.PushAsync(new PopupViewOperations());
                IsRunning = false;
            }
        }

        public OperationsViewModel()
        {
            ViewOperationCommand = new Command<object>(ViewClicked);

            _operationsList = new ObservableCollection<Models.POSFeature.OperationModel>();
            LoadOperation("");
            if (Local.PSACache.Instance.UserInfo.store_operation)
            {
                GetStarted = false;
            }
            else
            {
                GetStarted = true;
            }
        }

        public void LoadOperation(string opid)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _operationsList.Clear();
                    Http.MobilePOS.Operation.Instance.Listing((bool success, string data) =>
                    {
                        if (success)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            Models.POSFeature.OperationModel datas = JsonConvert.DeserializeObject<Models.POSFeature.OperationModel>(data);
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                DateTime date = DateTime.ParseExact(datas.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                                DateTime open = DateTime.ParseExact(datas.data[i].date_open, "HH:mm:ss", provider);
                                DateTime close = DateTime.ParseExact(datas.data[i].date_close, "HH:mm:ss", provider);
                                _operationsList.Add(new Models.POSFeature.OperationModel()
                                {
                                    Id = datas.data[i].ID,
                                    Opening = open.ToString("hh:mm tt"),
                                    Closing = close.ToString("hh:mm tt"),
                                    Date = "Date: " + date.ToString("MMMM dd, yyyy"),
                                    FullSchedule = date.ToString("MMMM dd, yyyy"),
                                    TotalSales = "Total Sales: " + datas.data[i].total_sale
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
                    new Controllers.Notice.Alert("Error Code: MPV2OPE-L1OVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2OPE-L1OVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2OPE-L1OVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2OPE-L1OVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

        public static void RefreshOperation()
        {
            try
            {
                _operationsList.Clear();
                Http.MobilePOS.Operation.Instance.Listing((bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Models.POSFeature.OperationModel datas = JsonConvert.DeserializeObject<Models.POSFeature.OperationModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            DateTime date = DateTime.ParseExact(datas.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime open = DateTime.ParseExact(datas.data[i].date_open, "HH:mm:ss", provider);
                            DateTime close = DateTime.ParseExact(datas.data[i].date_close, "HH:mm:ss", provider);
                            _operationsList.Add(new Models.POSFeature.OperationModel()
                            {
                                Id = datas.data[i].ID,
                                Opening = open.ToString("hh:mm tt"),
                                Closing = close.ToString("hh:mm tt"),
                                Date = "Date: " + date.ToString("MMMM dd, yyyy"),
                                FullSchedule = date.ToString("MMMM dd, yyyy"),
                                TotalSales = "Total Sales: " + datas.data[i].total_sale
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
                    new Controllers.Notice.Alert("Error Code: MPV2OPE-L1OVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2OPE-L1OVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2OPE-L1OVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2OPE-L1OVM-" + err.ToString());
                }
            }
        }

    }
}
