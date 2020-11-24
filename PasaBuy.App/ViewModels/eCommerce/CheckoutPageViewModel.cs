using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Local;
using PasaBuy.App.Models.eCommerce;
using PasaBuy.App.Views.eCommerce;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.eCommerce
{
    /// <summary>
    /// ViewModel for Checkout page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CheckoutPageViewModel : BaseViewModel
    {
        #region Fields
        public static string fees = string.Empty;
        public static string charges = string.Empty;
        public static double totalprice;
        public static double discount;
        public static double coupon;

        public static ObservableCollection<Customer> deliveryAddress;

        private ObservableCollection<Payment> paymentModes;

        //private ObservableCollection<Product> cartDetails;

        private double totalPrice;


        private double _totalSrp;

        private double discountPrice;

        private double discountPercent;

        public string deliveryFee = "Free";
        public static bool isClicked = false;
        public static int address_id = 0;
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

        /// <summary>
        /// Initializes a new instance for the <see cref="CheckoutPageViewModel" /> class.
        /// </summary>
        public CheckoutPageViewModel()
        {
            this.TotalPrice = totalprice;
            //this.DiscountPrice = discount;
            //this.DiscountPercent = coupon;
            this.DeliveryFee = charges;
            this.TotalSrp = totalprice;

            this.EditCommand = new Command(this.EditClicked);
            this.AddAddressCommand = new Command(this.AddAddressClicked);
            this.PlaceOrderCommand = new Command(this.PlaceOrderClicked);
            this.PaymentOptionCommand = new Command(PaymentOptionClicked);
            this.ApplyCouponCommand = new Command(this.ApplyCouponClicked);
            this.IsBusy = true;
            PaymentMethod();
            LoadCart();

            deliveryAddress.CollectionChanged += CollectionChanges;
        }

        private void CollectionChanges(object sender, EventArgs e)
        {
            try
            {
                Http.HatidPress.Order.Instance.DeliveryFee(AddressInMapPage.lat.ToString(), AddressInMapPage.lon.ToString(), Marketplace.StoreDetailsViewModel.store_id, (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.HatidFeature.Delivery fee = JsonConvert.DeserializeObject<Models.HatidFeature.Delivery>(data);
                        for (int i = 0; i < fee.data.Length; i++)
                        {
                            this.DeliveryFee = "₱ " + fee.data[i].price.ToString();
                            charges = "₱ " + fee.data[i].price.ToString();
                            fees = fee.data[i].price.ToString();
                            this.TotalPrice = totalprice + fee.data[i].price;
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
                    new Controllers.Notice.Alert("Error Code: HPV2ODR-F1CPVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2ODR-F1CPVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2ODR-F1CPVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2ODR-F1CPVM-" + err.ToString());
                }
            }
        }

        public static void LoadCart()
        {
            // TODO : REVISE WITH VARIANTS
            _ListOrder = new ObservableCollection<Models.MobilePOS.OrderModel>();
            _VariantList = new ObservableCollection<Models.MobilePOS.VariantModel>();
            _PaymentsList = new ObservableCollection<Models.MobilePOS.VariantModel>();

            foreach (var car in CartPageViewModel.cartDetails)
            {
                foreach (var vars in car.Variants)
                {
                    _VariantList.Add(new Models.MobilePOS.VariantModel()
                    {
                        ID = vars.Id,
                    });
                }
                foreach (var vars in car.Variants)
                {
                    _PaymentsList.Add(new Models.MobilePOS.VariantModel()
                    {
                        Method = "cash"
                    });
                }

                _ListOrder.Add(new Models.MobilePOS.OrderModel()
                {
                    ID = car.ID.ToString(),
                    TotalQuantity = Convert.ToInt32(car.Quantity),
                    Remarks = !string.IsNullOrEmpty(car.Remarks) || !string.IsNullOrWhiteSpace(car.Remarks) ? car.Remarks : "",
                    Variants = _VariantList,
                    Payments = _PaymentsList
                });
            }
        }

        public static void InsertAddress(string id, string person, string type, string fulladdress, string contact)
        {
            deliveryAddress.Clear();
            deliveryAddress.Add(new Customer
            {
                CustomerId = Convert.ToInt32(id),
                CustomerName = person,
                AddressType = type,
                Address = fulladdress,
                MobileNumber = contact
            });
        }
        public static void MyAddress()
        {
            try
            {
                Http.DataVice.Address.Instance.Listing( (bool success, string data) =>
                {
                    if (success)
                    {
                        Customer address = JsonConvert.DeserializeObject<Customer>(data);
                        if (address.data.Length > 0)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                address_id = Convert.ToInt32(address.data[i].id);
                                string types = address.data[i].types;
                                string type = string.Empty;
                                if (types == "home") { type = "Home"; }
                                if (types == "office") { type = "Office"; }
                                if (types == "business") { type = "Business"; }
                                deliveryAddress.Add(new Customer
                                {
                                    CustomerId = Convert.ToInt32(address.data[i].id),
                                    CustomerName = string.IsNullOrEmpty(address.data[i].contact_person) ? PSACache.Instance.UserInfo.dname : address.data[i].contact_person,
                                    AddressType = types,
                                    Address = address.data[i].street + " " + address.data[i].brgy + " " + address.data[i].city + " " + address.data[i].province + ", " + address.data[i].country,
                                    MobileNumber = address.data[i].contact
                                });
                            }
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
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-L1CPVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-L1CPVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-L1CPVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-L1CPVM-" + err.ToString());
                }
            }
        }
        public void PaymentMethod()
        {
            this.PaymentModes = new ObservableCollection<Payment>
            {
                /*new Payment
                {
                    PaymentMode = "Goldman Sachs Bank Credit Card", CardNumber = "48** **** **** 9876",
                    CardTypeIcon = "Card.png"
                },
                new Payment {PaymentMode = "Card Payment"},*/
                new Payment {PaymentMode = "Cash on Delivery"}, // CASH
                new Payment {PaymentMode = "Savings Wallet"}, // WALLET - REST - IF NO WALLET - MESSAGE / IF
                // EXTRAS - COUPONS(X) / PASABUY PLUS(/) (OPTIONAL)
                // IF COD = ENABLE PASABUY
                // IF WALLET - DISABLE PASABUY
            };
        }
        #endregion

        #region Public properties

        public static ObservableCollection<Models.MobilePOS.VariantModel> _VariantList;
        public ObservableCollection<Models.MobilePOS.VariantModel> VariantList
        {
            get
            {
                return _VariantList;
            }
            set
            {
                if (_VariantList == value)
                {
                    return;
                }

                _VariantList = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Models.MobilePOS.VariantModel> _PaymentsList;
        public ObservableCollection<Models.MobilePOS.VariantModel> PaymentsList
        {
            get
            {
                return _PaymentsList;
            }
            set
            {
                if (_PaymentsList == value)
                {
                    return;
                }

                _PaymentsList = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Models.MobilePOS.OrderModel> _ListOrder;
        public ObservableCollection<Models.MobilePOS.OrderModel> ListOrder
        {
            get
            {
                return _ListOrder;
            }
            set
            {
                if (_ListOrder == value)
                {
                    return;
                }

                _ListOrder = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the delivery address.
        /// </summary>
        /// 

        public ObservableCollection<Customer> DeliveryAddress
        {
            get
            {
                return deliveryAddress;
            }
            set
            {
                if (deliveryAddress == value)
                {
                    return;
                }

                deliveryAddress = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the payment modes.
        /// </summary>
        public ObservableCollection<Payment> PaymentModes
        {
            get
            {
                return this.paymentModes;
            }
            set
            {
                if (this.paymentModes == value)
                {
                    return;
                }

                this.paymentModes = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        /*public ObservableCollection<Product> CartDetails
        {
            get
            {
                return this.cartDetails;
            }
            set
            {
                if (this.cartDetails == value)
                {
                    return;
                }

                this.cartDetails = value;
                this.NotifyPropertyChanged();
            }
        }*/

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays total price.
        /// </summary>
        public double TotalPrice
        {
            get { return this.totalPrice; }

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

        public double TotalSrp
        {
            get { return this._totalSrp; }

            set
            {
                if (this._totalSrp == value)
                {
                    return;
                }

                this._totalSrp = value;
                this.NotifyPropertyChanged();
            }
        }


        public string DeliveryFee
        {
            get { return this.deliveryFee; }

            set
            {
                if (this.deliveryFee == value)
                {
                    return;
                }

                this.deliveryFee = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays total discount price.
        /// </summary>
        public double DiscountPrice
        {
            get { return this.discountPrice; }

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
        /// Gets or sets the property that has been bound with a label, which displays discount.
        /// </summary>
        public double DiscountPercent
        {
            get { return this.discountPercent; }

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

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Add new address button is clicked.
        /// </summary>
        public Command AddAddressCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command PlaceOrderCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the payment option button is clicked.
        /// </summary>
        public Command PaymentOptionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the apply coupon button is clicked.
        /// </summary>
        public Command ApplyCouponCommand { get; set; }

        private DelegateCommand changeAddressCommand;

        public DelegateCommand ChangeAddressCommand =>
           changeAddressCommand ?? (changeAddressCommand = new DelegateCommand(ChangeAddressClicked));
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Edit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void EditClicked(object obj)
        {
            // Do something
        }

        private async void ChangeAddressClicked(object obj)
        {
            if (!isRunning)
            {
                isRunning = true;
                await Task.Delay(200);
                await Application.Current.MainPage.Navigation.PushModalAsync(new ChangeAddressPage());
                isRunning = false;
            }
        }

        /// <summary>
        /// Invoked when the Add address button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddAddressClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Place order button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PlaceOrderClicked(object obj)
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    if (address_id == 0)
                    {
                        new Controllers.Notice.Alert("Notice to User", "Please select address.", "Try Again");
                        isRunning = false;
                    }
                    else if (PaymentView.method != string.Empty)
                    {
                        var btn = obj as Syncfusion.XForms.Buttons.SfButton;

                        /*foreach (var order in ListOrder)
                        {
                            _PaymentsList.Add(new Models.MobilePOS.VariantModel()
                            {
                                Method = PaymentView.method,
                            });

                            _ListOrder.Add(new Models.MobilePOS.OrderModel()
                            {
                                Payments = _PaymentsList
                            });
                            break;
                        }*/
                            
                            Http.MobilePOS.Order.Instance.Create(Marketplace.StoreDetailsViewModel.store_id, Marketplace.StoreDetailsViewModel.operation_id, address_id.ToString(), fees, PaymentView.method, ListOrder, async (bool success, string data) =>
                            {
                                if (success)
                                {
                                    fees = string.Empty;
                                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PaymentSuccessPage()));
                                    isRunning = false;
                                }
                                else
                                {
                                    new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    isRunning = false;
                                }
                            });
                        }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", "Please select payment method.", "Try Again");
                        isRunning = false;
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2ODR-I1CPVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ODR-I1CPVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-I1CPVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ODR-I1CPVM-" + err.ToString());
                }
                isRunning = false;
            }
        }

        /// <summary>
        /// Invoked when the Payment option is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void PaymentOptionClicked(object obj)
        {
            if (obj is RowDefinition rowDefinition && rowDefinition.Height.Value == 0)
            {
                rowDefinition.Height = GridLength.Auto;
            }
        }

        /// <summary>
        /// Invoked when the Apply coupon button is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ApplyCouponClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}