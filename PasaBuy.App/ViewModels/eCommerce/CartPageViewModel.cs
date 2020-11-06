using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.eCommerce;
using PasaBuy.App.Views.ErrorAndEmpty;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.eCommerce
{
    /// <summary>
    /// ViewModel for cart page.
    /// </summary>
    [Preserve(AllMembers = true)]
    //[DataContract]
    public class CartPageViewModel : BaseViewModel
    {
        #region Fields

        public static ObservableCollection<ProductList> cartDetails;

        private ObservableCollection<ProductList> produts;

        private double totalPrice;

        private double discountPrice;

        private string deliveryfee;

        private double discountPercent;

        private double percent;

        public bool isCartClicked = false;

        public static int refresh = 0;

        public static ObservableCollection<Options> _quantityChanged;
        public ObservableCollection<Options> QuantityChanged
        {
            get
            {
                return _quantityChanged;
            }

            set
            {
                if (_quantityChanged == value)
                {
                    return;
                }

                _quantityChanged = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool _isRunning = false;
        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public CartPageViewModel()
        {
            CheckoutPageViewModel.deliveryAddress = new ObservableCollection<Models.eCommerce.Customer>();

            cartDetails = new ObservableCollection<ProductList>(Marketplace.StoreDetailsViewModel.cartDetails);
            LoadCart();

            _quantityChanged = new ObservableCollection<Options>();
            _quantityChanged.CollectionChanged += CollectionChanges;
        }
        public void LoadCart()
        {
            double totalprice = 0;
            foreach (ProductList prod in cartDetails)
            {
                double varprice = 0;
                foreach (Options var in prod.Variants)
                {
                    varprice += var.Price;
                }
                totalprice += prod.Quantity * (prod.ActualPrice + varprice);
            }
            this.TotalPrice = totalprice;
            //this.DiscountPrice = totalprice;

            this.DeliveryFee = "0";
            Marketplace.StoreDetailsViewModel.Convert2String(Marketplace.StoreDetailsViewModel.store_id);
        }

        private void CollectionChanges(object sender, EventArgs e)
        {
            LoadCart();
        }

        public static void InsertCart(string storeid, string id, string name, string summary, string image, double price, int quantity)
        {
            bool itemExists = cartDetails.Any(item =>
            {
                return (item.ID == id);
            });
            if (!itemExists)
            {
                cartDetails.Insert(0, new ProductList()
                {
                    Stid = storeid,
                    ID = id,
                    Name = name,
                    Summary = summary,
                    Avatar = PSAProc.GetUrl(image),
                    ActualPrice = price,
                    TotalQuantity = quantity,
                    Quantity = quantity
                });
            }
            else
            {
                foreach (ProductList item in cartDetails)
                {
                    if (item.ID == id)
                    {
                        item.TotalQuantity = quantity;
                        item.Quantity = quantity;
                    }
                }
            }
            Convert2String(storeid);
        }
        public static void Convert2String(string stid)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cartDetails);
            Xamarin.Essentials.Preferences.Set(stid, json);
            //System.Diagnostics.Debug.WriteLine(json);
            //new Alert("Data", json, "OK");
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
        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the total price.
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return this.totalPrice;
            }

            set
            {
                if (this.totalPrice == value)
                {
                    return;
                }

                this.totalPrice = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays total discount price.
        /// </summary>
        public double DiscountPrice
        {
            get
            {
                return this.discountPrice;
            }

            set
            {
                if (this.discountPrice == value)
                {
                    return;
                }

                this.discountPrice = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays delivery fee.
        /// </summary>
        public string DeliveryFee
        {
            get
            {
                return this.deliveryfee;
            }

            set
            {
                if (this.deliveryfee == value)
                {
                    return;
                }

                this.deliveryfee = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays discount.
        /// </summary>
        public double DiscountPercent
        {
            get
            {
                return this.discountPercent;
            }

            set
            {
                if (this.discountPercent == value)
                {
                    return;
                }

                this.discountPercent = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with list view, which displays the collection of products from json.
        /// </summary>
        public ObservableCollection<ProductList> Products
        {
            get
            {
                return this.produts;
            }

            set
            {
                if (this.produts == value)
                {
                    return;
                }
                this.produts = value;
                this.NotifyPropertyChanged();
                this.GetProducts(Products);
                this.UpdatePrice();
            }
        }

        #endregion

        #region Command

        private Command placeOrderCommand;

        private Command removeCommand;

        private Command quantitySelectedCommand;

        private Command variantSelectedCommand;

        private Command applyCouponCommand;

        private Command backButtonCommand;

        private DelegateCommand changeAddressCommand;


        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command PlaceOrderCommand
        {
            get { return this.placeOrderCommand ?? (this.placeOrderCommand = new Command(this.PlaceOrderClicked)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command RemoveCommand
        {
            get { return this.removeCommand ?? (this.removeCommand = new Command(this.RemoveClicked)); }
        }
        /// <summary>
        /// Gets or sets the command that will be executed when the quantity is selected.
        /// </summary>
        public Command QuantitySelectedCommand
        {
            get { return this.quantitySelectedCommand ?? (this.quantitySelectedCommand = new Command(this.QuantitySelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the variant is selected.
        /// </summary>
        public Command VariantSelectedCommand
        {
            get { return this.variantSelectedCommand ?? (this.variantSelectedCommand = new Command(this.VariantSelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the apply coupon is clicked.
        /// </summary>
        public Command ApplyCouponCommand
        {
            get { return this.applyCouponCommand ?? (this.applyCouponCommand = new Command(this.ApplyCouponClicked)); }
        }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand
        {
            get { return this.backButtonCommand ?? (this.backButtonCommand = new Command(this.BackButtonClicked)); }
        }


        public DelegateCommand ChangeAddressCommand =>
            changeAddressCommand ?? (changeAddressCommand = new DelegateCommand(ChangeAddressClicked));
        #endregion

        #region Methods

        private void ChangeAddressClicked(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new ChangeAddressPage());
            new Alert("Ok", "ok", "ok");
        }


        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {
            if (CartDetails.Count > 0)
            {
                if (!isRunning)
                {
                    isRunning = true;
                    //CheckoutPageViewModel.coupon = this.DiscountPercent;
                    //CheckoutPageViewModel.discount = this.DiscountPrice;
                    CheckoutPageViewModel.totalprice = this.TotalPrice;
                    CheckoutPageViewModel.charges = this.DeliveryFee;
                    Marketplace.StoreDetailsViewModel.Convert2List(Marketplace.StoreDetailsViewModel.store_id);
                    CheckoutPageViewModel.LoadCart();
                    PaymentView.method = string.Empty;
                    await Task.Delay(200);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new CheckoutPage());
                    isRunning = false;
                }
            }
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void RemoveClicked(object obj)
        {
            if (!isRunning)
            {
                isRunning = true;
                if (obj is ProductList product)
                {
                    this.CartDetails.Remove(product);
                    LoadCart();

                    //this.UpdatePrice();
                    //Convert2String(product.stid);

                    Marketplace.StoreDetailsViewModel.cartDetails.Remove(product);
                    Marketplace.StoreDetailsViewModel.Convert2String(product.stid);

                    if (this.CartDetails.Count == 0)
                    {
                        await NavigateToPage(new EmptyCartPage());
                        isRunning = false;
                    }
                    else
                    {
                        isRunning = false;
                    }
                }
            }
        }
        async Task NavigateToPage(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        /// <summary>
        /// Invoked when the quantity is selected.
        /// </summary>
        /// <param name="selectedItem">The Object</param>
        private void QuantitySelected(object selectedItem)
        {
            //Incident - 249030 - Issue in ComboBox Slection changed event.

            /*var item = selectedItem as ProductList;

            this.UpdatePrice();
            item.ActualPrice = item.ActualPrice * item.TotalQuantity;
            item.DiscountPrice = item.DiscountPrice * item.TotalQuantity;*/
        }

        /// <summary>
        /// Invoked when the variant is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VariantSelected(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the apply coupon button is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ApplyCouponClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// This method is used to get the products from json.
        /// </summary>
        /// <param name="Products">The Products</param>
        private void GetProducts(ObservableCollection<ProductList> Products)
        {
            this.CartDetails = new ObservableCollection<ProductList>();
            if (Products != null && Products.Count > 0)
                this.CartDetails = Products;
        }

        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        public void UpdatePrice()
        {
            this.ResetPriceValue();

            if (this.CartDetails != null && this.CartDetails.Count > 0)
            {
                foreach (var cartDetail in this.CartDetails)
                {
                    if (cartDetail.TotalQuantity == 0)
                        cartDetail.TotalQuantity = 1;
                    this.TotalPrice += (cartDetail.ActualPrice * cartDetail.TotalQuantity);
                    //this.DiscountPrice += (cartDetail.DiscountPrice * cartDetail.TotalQuantity);
                    //this.percent += cartDetail.DiscountPercent;
                }
                //this.DiscountPercent = this.percent > 0 ? this.percent / this.CartDetails.Count : 0;
            }
        }

        /// <summary>
        /// This method is used to reset the price amount.
        /// </summary>
        private void ResetPriceValue()
        {
            this.TotalPrice = 0;
            //this.DiscountPercent = 0;
            //this.DiscountPrice = 0;
            //this.percent = 0;
        }

        #endregion
    }
}
