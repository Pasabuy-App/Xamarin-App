using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PasaBuy.App.Models.eCommerce;
using PasaBuy.App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using PasaBuy.App.Local;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.eCommerce;
using System;
using MobilePOS;
using PasaBuy.App.ViewModels.Marketplace;
using Newtonsoft.Json;
using PasaBuy.App.Models.Marketplace;
using System.Threading.Tasks;
using PasaBuy.App.Commands;

namespace PasaBuy.App.ViewModels.eCommerce
{
    /// <summary>
    /// ViewModel for Checkout page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CheckoutPageViewModel : BaseViewModel
    {
        #region Fields
        public static string charges = string.Empty;
        public static double totalprice;
        public static double discount;
        public static double coupon;

        public static ObservableCollection<Customer> deliveryAddress;

        private ObservableCollection<Payment> paymentModes;

        private ObservableCollection<Product> cartDetails;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        public string deliveryFee = "Free";
        public static bool isClicked = false;
        public static int address_id = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CheckoutPageViewModel" /> class.
        /// </summary>
        public CheckoutPageViewModel()
        {
            this.TotalPrice = totalprice;
            this.DiscountPrice = discount;
            this.DiscountPercent = coupon;
            this.DeliveryFee = charges;

            this.EditCommand = new Command(this.EditClicked);
            this.AddAddressCommand = new Command(this.AddAddressClicked);
            this.PlaceOrderCommand = new Command(this.PlaceOrderClicked);
            this.PaymentOptionCommand = new Command(PaymentOptionClicked);
            this.ApplyCouponCommand = new Command(this.ApplyCouponClicked);
            this.IsBusy = true;
            PaymentMethod();
            MyAddress();
        }
        public static void MyAddress()
        {
            try
            {
                deliveryAddress = new ObservableCollection<Customer>();
                deliveryAddress.Clear();
                DataVice.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
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
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        public void PaymentMethod()
        {
            this.PaymentModes = new ObservableCollection<Payment>
            {
                new Payment {PaymentMode = "Cash on Delivery"},
                //new Payment {PaymentMode = "Wallet"},
            };
        }
        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with SfListView, which displays the delivery address.
        /// </summary>
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
        public ObservableCollection<Product> CartDetails
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
        }

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
            IsBusy = false;
            await Application.Current.MainPage.Navigation.PushModalAsync(new ChangeAddressPage());
            IsBusy = true;
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
        private async void PlaceOrderClicked(object obj)
        {
            try
            {
                if (PaymentView.method != string.Empty)
                {
                    var btn = obj as Syncfusion.XForms.Buttons.SfButton;
                    if (btn.IsEnabled)
                    {
                        btn.IsEnabled = false;
                        //Console.WriteLine("Method: " + PaymentView.method + " Address ID: " + address_id); //address id in first selection in 
                        foreach (var car in CartPageViewModel.cartDetails)
                        {
                            //Console.WriteLine("Method: " + PaymentView.method + " Address ID: " + address_id + " ." + car.Stid.ToString() + ". ." + car.ID.ToString() + ". ." + car.TotalQuantity.ToString() + ".");
                            Customers.Instance.Create(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, car.Stid.ToString(), car.ID.ToString(), "1", car.TotalQuantity.ToString(), "", PaymentView.method, address_id.ToString(), (bool success, string data) =>
                            {
                                if (success)
                                {
                                    (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PaymentSuccessPage()));
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                }
                            });
                        }
                        await Task.Delay(500);
                        btn.IsEnabled = true;
                    }   
                }
                else
                {
                    new Alert("Notice to User", "Please select payment method.", "Try Again");
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
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