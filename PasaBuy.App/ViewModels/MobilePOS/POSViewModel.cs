using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.StoreViews.POS;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class POSViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.MobilePOS.PointOfSales> _currentOrder;

        public static ObservableCollection<Models.MobilePOS.ProductData> productsList;


        public  ObservableCollection<Models.MobilePOS.PointOfSales> CurrentOrder
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
            this.AddOrderProductCommand = new Command(this.AddOrderProductClicked);
            productsList = new ObservableCollection<ProductData>();
            _currentOrder = new ObservableCollection<Models.MobilePOS.PointOfSales>();
            LoadProduct();
            _currentOrder.Add(new Models.MobilePOS.PointOfSales()
            {
                Name = "Test Item",
                Price = 120,
                Id = "123"
            });

        }

        private ICommand _addToOrderCommand;

        public ICommand AddToOrderCommand => _addToOrderCommand ?? (_addToOrderCommand = new Command<string>(AddToOrder));

        private async void LoadProduct()
        {
            IsBusy = true;
            try
            {
                TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        ProductData product = JsonConvert.DeserializeObject<ProductData>(data);
                        for (int i = 0; i < product.data.Length; i++)
                        {
                            string id = product.data[i].ID;
                            string product_name = product.data[i].product_name;
                            string short_info = product.data[i].short_info;
                            float price = (float) Convert.ToDouble(product.data[i].price);
                            string preview = product.data[i].preview;
                            productsList.Add(new ProductData()
                            {
                                ID = id,
                                Product_name = product_name,
                                Short_info = short_info,
                                Price = price,
                                Preview = PSAProc.GetUrl(preview)
                            });
                        }
                        IsBusy = false;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        IsBusy = false;
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }

        private void AddToOrder(string selectedItemId)
        {
            var item = ProductsList.FirstOrDefault(x => x.ID.Equals(selectedItemId));


            _currentOrder.Add(new Models.MobilePOS.PointOfSales
            {
                Name = "Test Item34",
                Price = 120,
                Id = "123"
            });

            _currentOrder.Add(new Models.MobilePOS.PointOfSales
            {
                Name = item.Product_name,
                Price = item.Price,
                Id = selectedItemId
            });

            Console.WriteLine(item.Price + item.Product_name + item.ID);
            Application.Current.MainPage.Navigation.PopModalAsync();


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
