using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class TransactionHistoryViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.Driver.TransactListData> _HistoryList;
        public ObservableCollection<Models.Driver.TransactListData> HistoryList
        {
            get
            {
                return _HistoryList;
            }
            set
            {
                _HistoryList = value;
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
        public ICommand RefreshCommand { protected set; get; }

        public static int LastIndex = 11;
        public TransactionHistoryViewModel()
        {
            _HistoryList = new ObservableCollection<Models.Driver.TransactListData>();
            _HistoryList.Clear();
            LoadData("");
            RefreshCommand = new Command<string>((key) =>
            {
                LastIndex = 11;
                _HistoryList.Clear();
                LoadData("");
                IsRefreshing = false;
            });
        }

        public void LoadData(string offset)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Http.HatidPress.Order.Instance.Listing_Order( (bool success, string data) =>
                    {
                        if (success)
                        {
                            CultureInfo provider = new CultureInfo("fr-FR");
                            Models.Driver.TransactListData order = JsonConvert.DeserializeObject<Models.Driver.TransactListData>(data);
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                string dates = string.IsNullOrEmpty(order.data[i].date_created) ? "0000-00-00 00:00:00" : order.data[i].date_created;
                                DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);
                                _HistoryList.Add(new Models.Driver.TransactListData()
                                {
                                    ID = order.data[i].order_id,
                                    Customer = order.data[i].customer_name,
                                    Store_logo = Local.PSAProc.GetUrl(order.data[i].customer_avatar),
                                    Hash_id = "Order #"+ order.data[i].order_id,
                                    Date_created = date.ToString("MMMM dd, yyyy"),
                                    Price = Convert.ToDouble(order.data[i].total_price),
                                    Method = order.data[i].method,
                                    Status = order.data[i].stages
                                });
                            }
                            IsBusy = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsBusy = false;
                        }
                    });
                }
            }

            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2ODR-L1THVM.", "OK");
                IsBusy = false;
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
            if (!IsBusy)
            {
                IsBusy = true;
                var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Models.Driver.TransactListData;
                MobilePOS.OrderDetailsViewModel.order_id = item.ID;
                MobilePOS.OrderDetailsViewModel.customer = item.Customer;
                MobilePOS.OrderDetailsViewModel.avatar = item.Store_logo;
                MobilePOS.OrderDetailsViewModel.datecreated = item.Date_created;
                MobilePOS.OrderDetailsViewModel.totalprice = item.Price;
                MobilePOS.OrderDetailsViewModel.stages = item.Status;
                MobilePOS.OrderDetailsViewModel.method = item.Method;
                await Application.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());
                IsBusy = false;
            }
        }

    }
}