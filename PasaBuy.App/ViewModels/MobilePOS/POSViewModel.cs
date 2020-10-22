using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.StoreViews.POS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class POSViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.MobilePOS.PointOfSales> _currentOrder;

        public static ObservableCollection<Models.MobilePOS.ProductData> productsList;

        public ICommand AddToOrderCommand
        {
            get
            {
                return new Command<string>((x) => AddToOrder(x));
            }
        }

        private async void AddToOrder(string id)
        {
            new Alert("ok", id, "ok");
            await (App.Current.MainPage).Navigation.PopModalAsync();
        }

        public ObservableCollection<Models.MobilePOS.PointOfSales> CurrentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Models.MobilePOS.ProductData> ProductsList
        {
            get 
            { 
                return productsList;
            }
            set 
            {
                productsList = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public POSViewModel()
        {

            //this.ProductsList = new ObservableCollection<Models.MobilePOS.ProductData>()
            //{
            //    new Models.MobilePOS.ProductData
            //    {
            //        ID = "1",
            //        Product_name = "Snickers"
            //    },
            //    new Models.MobilePOS.ProductData
            //    {
            //        ID = "13",
            //        Product_name = "Strawberry Shake"
            //    },
            //    new Models.MobilePOS.ProductData
            //    {
            //        ID = "15",
            //        Product_name = "Chocolate Shake"
            //    },
            //    new Models.MobilePOS.ProductData
            //    {
            //        ID = "21",
            //        Product_name = "Milk Shake"
            //    },
            //    new Models.MobilePOS.ProductData
            //    {
            //        ID = "51",
            //        Product_name = "C2 Apple"
            //    },
            //};
            //this.CurrentOrder = new ObservableCollection<Models.MobilePOS.PointOfSales>()
            //{
            //    new Models.MobilePOS.PointOfSales
            //    {
            //        Name = "Cheeseburger"
            //    },
            //    new Models.MobilePOS.PointOfSales
            //    {
            //        Name = "Taco Chips"
            //    },
            //    new Models.MobilePOS.PointOfSales
            //    {
            //        Name = "Galaxy Drink"
            //    },
            //};

            this.AddOrderProductCommand = new Command(this.AddOrderProductClicked);
        }

        public Command AddOrderProductCommand
        {
            get;
            set;
        }

        private async void AddOrderProductClicked(object obj)
        {
            await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new SelectProduct()));
        }
    }
}
