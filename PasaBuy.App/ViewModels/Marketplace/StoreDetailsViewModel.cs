﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.eCommerce;
using PasaBuy.App.Views.ErrorAndEmpty;
using System;
using System.Collections.ObjectModel;
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
        bool _IsRunning = false;
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

        public static string store_id = string.Empty;
        public static string title = string.Empty;
        public static string address = string.Empty;
        public static string info = string.Empty;
        public static string banner = string.Empty;
        public static string operation_id = string.Empty;

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
            //this.CartItemCount = CartPageViewModel.cartDetails.Count;Pr
            //CartPageViewModel.cartDetails.CollectionChanged += CollectionChanges;
            LoadDetails(store_id);
            Loadcategory(store_id);
            AddToCartCommand = new Command<object>(AddToCartClicked);

        }
        public void LoadDetails(string stid)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing(stid, "", "", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                        for (int i = 0; i < store.data.Length; i++)
                        {
                            this.StoreName = store.data[i].title;
                            this.StoreAddress = store.data[i].street + " " + store.data[i].brgy + " " + store.data[i].city + ", " + store.data[i].province;
                            this.AboutStore = !string.IsNullOrEmpty(store.data[i].info) ? store.data[i].info : "No information found.";
                            this.StoreBanner = store.data[i].banner == "None" || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(store.data[i].banner);
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
                    new Controllers.Notice.Alert("Error Code: TPV2STR-L1SDVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2STR-L1SDVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1SDVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2STR-L1SDVM-" + err.ToString());
                }
            }
        }

        public static void InsertCart(string id, string name, string summary, string image, double price, double discount, string remarks, int quantity, ObservableCollection<Options> Variants)
        {
            cartDetails.Insert(0, new ProductList()
            {
                Stid = store_id,
                ID = id,
                Name = name,
                Summary = summary,
                Avatar = image,
                ActualPrice = price,
                TotalQuantity = quantity,
                DiscountPercent = discount,
                Quantity = quantity,
                Remarks = remarks,
                Variants = Variants

            });
            Convert2String(store_id);
        }
        public static void Convert2String(string stid)
        {
            string json = JsonConvert.SerializeObject(cartDetails);
            Xamarin.Essentials.Preferences.Set(stid, json);
        }
        public static void Convert2List(string stid)
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(stid))
            {
                string data = Xamarin.Essentials.Preferences.Get(stid, "{}");
                var result = JsonConvert.DeserializeObject<System.Collections.Generic.List<ProductList>>(data);

                cartDetails = new ObservableCollection<ProductList>(result);
            }
        }

        private void CollectionChanges(object sender, EventArgs e)
        {
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

        public Command<object> AddToCartCommand { get; set; }

        private void AddToCartClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var product = obj as ProductList;
                ProductDetailViewModel.productid = product.ID;
                ProductDetailViewModel.productname = product.Name;
                ProductDetailViewModel.productimage = product.Avatar;
                ProductDetailViewModel.shortinfo = product.Description;
                ProductDetailViewModel.price = product.ActualPrice;
                ProductDetailViewModel.discount = product.DiscountPercent;
                Application.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.ProductDetail());
                IsRunning = false;
            }
        }

        async void GoToCartClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                await Task.Delay(500);
                if (this.cartItemCount != 0)
                {
                    await NavigateToPage(new CartPage());
                    IsRunning = false;
                }
                else
                {
                    await NavigateToPage(new EmptyCartPage());
                    IsRunning = false;
                }
            }

        }

        async Task NavigateToPage(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public void Loadcategory(string stid)
        {
            try
            {
                IsRunning = true;
                Http.TindaPress.Category.Instance.ProductPerCategory(stid, (bool success, string data) =>
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
                                        Avatar = PSAProc.GetUrl(catRoot.data[i].products[j].avatar),
                                        Name = catRoot.data[i].products[j].title,
                                        ActualPrice = Convert.ToDouble(catRoot.data[i].products[j].price),
                                        DiscountPercent = Convert.ToDouble(catRoot.data[i].products[j].discount),
                                        Description = catRoot.data[i].products[j].info
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
                       
                        IsRunning = false;
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        IsRunning = false;
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-PPC1SDVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-PPC1SDVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-PPC1SDVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-PPC1SDVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

    }
}