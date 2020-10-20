﻿
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.eCommerce;
using PasaBuy.App.Views.eCommerce;
using PasaBuy.App.Views.ErrorAndEmpty;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Marketplace
{
    /// <summary>
    /// ViewModel for Article parallax page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class StoreDetailsViewModel : BaseViewModel
    {
        #region Fields

        public static string store_id = string.Empty;

        private ObservableCollection<Categories> _storeData;

        private ObservableCollection<ProductList> productsList;

        private int cartItemCount = 0;

        private Boolean isCartBusy;
        public bool isCartClicked = false;

        private bool _isAdded = true;

        private bool _notAdded = false;
        public bool IsAdded
        {
            get
            {
                return this._isAdded;
            }
            set
            {
                this._isAdded = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool NotAdded
        {
            get
            {
                return this._notAdded;
            }
            set
            {
                this._notAdded = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ProductList> ProductsList
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

        public ObservableCollection<Categories> StoreData
        {
            get
            {
                return _storeData;
            }
            set
            {
                _storeData = value;
                this.NotifyPropertyChanged();
            }
        }

        public string store_name;
        public string StoreName
        {
            get { return store_name; }
            set
            {
                store_name = value;
                this.NotifyPropertyChanged();
            }
        }

        public string store_address;
        public string StoreAddress
        {
            get { return store_address; }
            set
            {
                store_address = value;
                this.NotifyPropertyChanged();
            }
        }

        public string about_store;
        public string AboutStore
        {
            get { return about_store; }
            set
            {
                about_store = value;
                this.NotifyPropertyChanged();
            }
        }

        public string store_banner;
        public string StoreBanner
        {
            get { return store_banner; }
            set
            {
                store_banner = value;
                this.NotifyPropertyChanged();
            }
        }

        public int CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        public Boolean IsCartBusy
        {
            get
            {
                return this.isCartBusy;
            }
            set
            {
                this.isCartBusy = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<ProductList> cartDetails;
        public ObservableCollection<ProductList> CartDetails
        {
            get
            {
                return cartDetails;
            }

            set
            {
                if (cartDetails == value)
                {
                    return;
                }

                cartDetails = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion
        public StoreDetailsViewModel()
        {
            _storeData = new ObservableCollection<Categories>();
            cartDetails = new ObservableCollection<ProductList>();
            Convert2List(store_id);
            this.CartItemCount = cartDetails.Count;
            cartDetails.CollectionChanged += CollectionChanges;

            //CartPageViewModel.cartDetails = new ObservableCollection<ProductList>();
            //CartPageViewModel.Convert2List(store_id);
            //this.CartItemCount = CartPageViewModel.cartDetails.Count;
            //CartPageViewModel.cartDetails.CollectionChanged += CollectionChanges;

            Loadcategory(store_id);
            LoadStoreDetails(store_id);

        }
        public static void InsertCart(string id, int vrid, double vrid_price, string name, string summary, string image, double price, int quantity)
        {
            cartDetails.Insert(0, new ProductList()
            {
                Stid = store_id,
                Vrid = vrid,
                Vrid_Price = vrid_price,
                ID = id,
                Name = name,
                Summary = summary,
                PreviewImage = PSAProc.GetUrl(image),
                ActualPrice = price,
                TotalQuantity = quantity,
                Quantity = quantity
            });
            Convert2String(store_id);
        }
        public static void Convert2String(string stid)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cartDetails);
            Xamarin.Essentials.Preferences.Set(stid, json);
        }
        public static void Convert2List(string stid)
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(stid))
            {
                string data = Xamarin.Essentials.Preferences.Get(stid, "{}");
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<ProductList>>(data);

                cartDetails = new ObservableCollection<ProductList>(result);
            }
        }

        private void CollectionChanges(object sender, EventArgs e)
        {
            //this.CartItemCount = CartPageViewModel.cartDetails.Count;
            this.CartItemCount = cartDetails.Count;
        }

        ICommand goToCartCommand;
        public ICommand GoToCartCommand
        {
            get
            {
                if (goToCartCommand == null)
                    goToCartCommand = new Command(GoToCartClicked, (x) => CanNavigate);

                return goToCartCommand;
            }
        }

        public ICommand AddToCartCommand
        {
            get
            {
                return new Command<string>((x) => AddToCartClicked(x));
            }
        }

        private async void AddToCartClicked(string id)
        {
            try
            {
                if (!isCartClicked)
                {
                    isCartClicked = true;
                    TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, store_id, "", id, "1", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            ProductListData datas = JsonConvert.DeserializeObject<ProductListData>(data);
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                ProductDetailViewModel.productid = datas.data[i].ID.ToString();
                                ProductDetailViewModel.productname = datas.data[i].product_name.ToString();
                                ProductDetailViewModel.shortinfo = datas.data[i].short_info.ToString();

                                ProductDetailViewModel.productimage = PSAProc.GetUrl(datas.data[i].preview.ToString());
                                ProductDetailViewModel.price = datas.data[i].price.ToString();
                            }
                            Application.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.ProductDetail());
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                    await Task.Delay(1000);
                    isCartClicked = false;
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        async void GoToCartClicked(object obj)
        {
            IsBusy = true;
            if (!isCartClicked)
            {
                if (this.cartItemCount != 0)
                {
                    isCartClicked = true;
                    CanNavigate = false;
                    IsCartBusy = true;
                    await NavigateToPage(new CartPage());
                    CanNavigate = true;
                    IsCartBusy = false;
                    await Task.Delay(100);
                    isCartClicked = false;
                    IsBusy = false;
                }
                else
                {
                    isCartClicked = true;
                    CanNavigate = false;
                    IsCartBusy = true;
                    IsBusy = true;
                    await NavigateToPage(new EmptyCartPage());
                    CanNavigate = true;
                    IsCartBusy = false;
                    IsBusy = false;
                    await Task.Delay(100);
                    isCartClicked = false;
                }
            }

        }

        async Task NavigateToPage(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public void LoadStoreDetails(string stid)
        {
            try
            {
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", stid, "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreDetailListData datas = JsonConvert.DeserializeObject<StoreDetailListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            this.StoreName = datas.data[i].title;
                            this.StoreAddress = datas.data[i].street + " " + datas.data[i].brgy + " " + datas.data[i].city + " " + datas.data[i].province;
                            this.AboutStore = !string.IsNullOrEmpty(datas.data[i].short_info) ? datas.data[i].short_info : "No information found.";
                            this.StoreBanner = PSAProc.GetUrl(datas.data[i].banner); // "https://pasabuy.app/wp-content/uploads/2020/10/Motorcycle.png";
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

        public void Loadcategory(string stid)
        {
            IsBusy = true;
            try
            {
                TindaPress.Category.Instance.ProductList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", stid, "", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        
                        Root catRoot = JsonConvert.DeserializeObject<Root>(data);
                        for (int i = 0; i < catRoot.data.Length; i++)
                        {
                            if (catRoot.data[i].products.Count != 0)
                            {
                                productsList = new ObservableCollection<ProductList>();
                                productsList.Clear();
                                for (int j = 0; j < catRoot.data[i].products.Count; j++)
                                {
                                    productsList.Add(new ProductList()
                                    {
                                        ID = catRoot.data[i].products[j].ID,
                                        PreviewImage = PSAProc.GetUrl(catRoot.data[i].products[j].preview) == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png" : PSAProc.GetUrl(catRoot.data[i].products[j].preview),
                                        Name = catRoot.data[i].products[j].product_name,
                                        ActualPrice = Convert.ToDouble(catRoot.data[i].products[j].price),
                                        Description = catRoot.data[i].products[j].short_info 
                                    });
                                }
                              
                                _storeData.Add(new Categories()
                                {
                                    Id = catRoot.data[i].ID,
                                    Info = catRoot.data[i].info,
                                    Title = catRoot.data[i].title,
                                    Prods = productsList
                                });
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
            IsBusy = false;
        }

    }
}