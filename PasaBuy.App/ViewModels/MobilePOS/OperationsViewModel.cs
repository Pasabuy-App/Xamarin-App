using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using MobilePOS;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using System.Globalization;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OperationsViewModel : BaseViewModel
    {
        public static ObservableCollection<Operations> _daysOfTheWeek;

        public static ObservableCollection<Operations> _operationsList;

        //public bool is_online;

        public ICommand ViewOperationCommand
        {
            get
            {
                return new Command<string>((x) => ViewOperation(x));
            }
        }

        private async void ViewOperation(string id)
        {
            //new Alert("ok", id, "ok");
            PopupViewOperations.opid = id;
            await PopupNavigation.Instance.PushAsync(new PopupViewOperations());
        }

        /*public bool IsOnline
        {
            get
            {
                return is_online;
            }
            set
            {
                is_online = value;
                this.NotifyPropertyChanged();
            }
        }*/

        public ObservableCollection<Operations> DaysOfTheWeek
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

        public ObservableCollection<Operations> OperationsList
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
        private void Submit()
        {
            GetStarted = false;
            Local.PSACache.Instance.UserInfo.store_operation = true;
            Local.PSACache.Instance.SaveUserData();
        }

        public OperationsViewModel()
        {
            //this.IsOnline = true;

            _operationsList = new ObservableCollection<Operations>();
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
        
        public static void LoadOperation(string opid)
        {
            try
            {
                _operationsList.Clear();
                Operation.Instance.ListByID_Sales(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, opid, (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Operations datas = JsonConvert.DeserializeObject<Operations>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string dates = string.IsNullOrEmpty(datas.data[i].date) ? "0000-00-00 00:00:00" : datas.data[i].date;
                            DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);
                            _operationsList.Add(new Operations()
                            {
                                Id = datas.data[i].ID,
                                Date = "Date: " + date.ToString("MMMM dd, yyyy"),
                                TotalSales = "Total Sales: " + datas.data[i].total_sale
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

    }
}
