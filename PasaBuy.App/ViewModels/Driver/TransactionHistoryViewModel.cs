using System;
using System.Collections.ObjectModel;
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
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "0",
                        Store_logo = "Avatar.png",
                        Customer = "Please revised rest api.",
                        Hash_id = "Order #0000",
                        Date_created = "Oct. 20, 2020 06:33 PM",
                        Price = 0,
                        Status = "Pending"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "1",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "10",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "11",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "24",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "35",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "46",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "57",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "68",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "79",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "81",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    _HistoryList.Add(new Models.Driver.TransactListData()
                    {
                        ID = "92",
                        Store_logo = "Avatar.png",
                        Customer = "Name",
                        Hash_id = "Order #4323",
                        Date_created = "Sept. 5, 2020 10:23 AM",
                        Price = 250,
                        Status = "Completed"
                    });
                    IsBusy = false;
                }
            }

            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
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
            //string id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Models.Driver.TransactListData).ID;
            //new Controllers.Notice.Alert("There is no ViewDetailsPage", "This is the Transaction ID : " + id, "OK"); // 27 ORDER ID
            var item = (selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Models.Driver.TransactListData;
            Views.StoreViews.TransactionDetailsView.id = item.ID;
            Views.StoreViews.TransactionDetailsView.customer = item.Customer;
            Views.StoreViews.TransactionDetailsView.orderid = item.Hash_id;
            Views.StoreViews.TransactionDetailsView.totalprice = item.Price.ToString();
            Views.StoreViews.TransactionDetailsView.datecreated = item.Date_created;
            Views.StoreViews.TransactionDetailsView.method = "Cash";// item.Method;
            Views.StoreViews.TransactionDetailsView.order_type = "completed";
            Views.StoreViews.TransactionDetailsView.stage_type = "completed";
            ViewModels.MobilePOS.OrderDetailsViewModel.LoadOrder("completed", "27", "");
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.StoreViews.TransactionDetailsView());
        }

    }
}
