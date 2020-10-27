using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.StoreViews.POS;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class POSViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.MobilePOS.PointOfSales> _currentOrder;
        public ObservableCollection<Models.MobilePOS.PointOfSales> CurrentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                if (_currentOrder == value)
                {
                    return;
                }
                _currentOrder = value;
                this.NotifyPropertyChanged();
            }
        }
        public static ObservableCollection<Models.MobilePOS.PointOfSales> _quantity;
        public ObservableCollection<Models.MobilePOS.PointOfSales> Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity == value)
                {
                    return;
                }
                _quantity = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _bill = string.Empty;
        public string Bill
        {
            get
            {
                return _bill;
            }
            set
            {
                if (_bill == value)
                {
                    return;
                }
                _bill = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _tax = string.Empty;
        public string Tax
        {
            get
            {
                return _tax;
            }
            set
            {
                if (_tax == value)
                {
                    return;
                }
                _tax = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _total = string.Empty;
        public string Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total == value)
                {
                    return;
                }
                _total = value;
                this.NotifyPropertyChanged();
            }
        }

        public POSViewModel()
        {
            this.AddOrderProductCommand = new Command(this.AddOrderProductClicked);
            DeleteCommand = new Command<object>(OnTapped);
            _currentOrder = new ObservableCollection<Models.MobilePOS.PointOfSales>();
            _quantity = new ObservableCollection<Models.MobilePOS.PointOfSales>();
            _currentOrder.Clear();
            this.Tax = "₱ 0.00";
            this.Bill = "₱ 0.00";
            this.Total = "₱ 0.00";

            _currentOrder.CollectionChanged += CollectionChanges;
            _quantity.CollectionChanged += CollectionChanges;
        }
        private void CollectionChanges(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        public void UpdatePrice()
        {
            double totalprice = 0;
            foreach (Models.MobilePOS.PointOfSales pos in _currentOrder)
            {
                totalprice += (pos.Price * pos.Quantity);
            }
            this.Tax = "₱ 0.00";
            this.Bill = "₱ " + totalprice.ToString();
            this.Total = "₱ " + totalprice.ToString();
        }

        public static void InsertData(string product_id, string product_name, double price, int quantity)
        {
            _currentOrder.Insert(0, new Models.MobilePOS.PointOfSales()
            {
                Id = product_id,
                Name = product_name,
                Price = Convert.ToSingle(price),
                Quantity = quantity
            });
        }

        public Command<object> DeleteCommand { get; set; }

        private void OnTapped(object obj)
        {
            var pos = obj as Models.MobilePOS.PointOfSales;
            _currentOrder.Remove(pos);
            App.Current.MainPage.DisplayAlert("Message", "Item Deleted :" + pos.Name, "Ok");
        }

        public Command AddOrderProductCommand
        {
            get;
            set;
        }

        private async void AddOrderProductClicked(object obj)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new SelectProduct()));
                IsBusy = false;
            }
        }
    }
}
