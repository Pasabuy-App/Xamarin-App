using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews.POS;
using Rg.Plugins.Popup.Services;
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

        private ObservableCollection<Variants> _variantsList;

        private ObservableCollection<Options> _optionsList;

        public ObservableCollection<Options> OptionsList
        {
            get
            {
                return _optionsList;
            }
            set
            {
                _optionsList = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Variants> VariantsList
        {
            get
            {
                return _variantsList;
            }
            set
            {
                _variantsList = value;
                this.NotifyPropertyChanged();
            }
        }


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
            _variantsList = new ObservableCollection<Variants>();

            ObservableCollection<Options> size_options = new ObservableCollection<Options>();
            size_options.Add(new Options() { Name = "Medium", Price = +25.00 });
            size_options.Add(new Options() { Name = "Large", Price = +45.00 });
            size_options.Add(new Options() { Name = "Grande", Price = +65.00 });
            ObservableCollection<Options> sweetness_options = new ObservableCollection<Options>();
            sweetness_options.Add(new Options() { Name = "25%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "50%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "75%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "100%", Price = +0.00 });

            _variantsList.Add(new Variants() { Name = "Size", options = size_options });
            _variantsList.Add(new Variants() { Name = "Sweetness Level", options = sweetness_options });

        }

        private ICommand _showVariants;

        private ICommand _addToOrder;

        public ICommand AddToOrderCommand => _addToOrder ?? (_addToOrder = new Command<string>(AddToOrderClicked));

        

        public ICommand ShowVariantsCommand => _showVariants ?? (_showVariants = new Command<string>(ShowVariantsClicked));

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


        private void AddToOrderClicked(string selectedItemId)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PopModalAsync();
            });
           
        }

        private void ShowVariantsClicked(string selectedItemId)
        {
            var item = ProductsList.FirstOrDefault(x => x.ID.Equals(selectedItemId));

            //Step 1: Get item.id and use it to get variants

            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new PopupShowVariants());
            });

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
