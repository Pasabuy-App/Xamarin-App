using Newtonsoft.Json;
using PanCardView.Extensions;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class ProductDetailViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<Variants> _variantsList;
        private ObservableCollection<Options> _optionsList;
        private ObservableCollection<Options> _addonsList;

        public ObservableCollection<Options> AddonsList
        {
            get
            {
                return _addonsList;
            }
            set
            {
                _addonsList = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Options> OptionsList
        {
            get
            {
                GroupKey = new SfRadioGroupKey();

                if (_optionsList != null)
                {
                    foreach (var item in _optionsList)
                    {
                        item.GroupKey = GroupKey;
                    }
                }
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

        public string _ProductImage;
        public string ProductImage
        {
            get { return _ProductImage; }
            set
            {
                _ProductImage = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                _ProductName = value;
                this.NotifyPropertyChanged();
            }
        }

        public double _Price;
        public double Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _Short_Info;
        public string Short_Info
        {
            get { return _Short_Info; }
            set
            {
                _Short_Info = value;
                this.NotifyPropertyChanged();
            }
        }

        public double _TotalPrice;
        public double TotalPrice
        {
            get { return _TotalPrice; }
            set
            {
                _TotalPrice = value;
                this.NotifyPropertyChanged();
            }
        }

        public int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                this.NotifyPropertyChanged();
            }
        }

        public int _TotalItems;
        public int TotalItems
        {
            get { return _TotalItems; }
            set
            {
                _TotalItems = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _SpecialInstructions;
        public string SpecialInstructions
        {
            get { return _SpecialInstructions; }
            set
            {
                _SpecialInstructions = value;
                this.NotifyPropertyChanged();
            }
        }

        public static string productid;
        public static string productname;
        public static string shortinfo;
        public static string productimage;
        public static string price;
        private bool isChecked = false;

        public SfRadioGroupKey GroupKey { get; set; }

        public static ObservableCollection<ProductList> _itemList;
        public ObservableCollection<ProductList> ItemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                if (_itemList == value)
                {
                    return;
                }
                _itemList = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion
        public ProductDetailViewModel()
        {
            _itemList = new ObservableCollection<ProductList>();
            _variantsList = new ObservableCollection<Variants>();
            _addonsList = new ObservableCollection<Options>();




            this.ProductName = productname;
            this.Price = Convert.ToDouble(price);
            this.Short_Info = shortinfo;
            this.ProductImage = productimage;

            this.Quantity = 1;
            //this.SpecialInstructions = "Special Instructions";

            /*this.TotalItems = 0;
            this.TotalPrice = 0.00;*/

            LoadVariants(productid);
            StoreDetailsViewModel.cartDetails.CollectionChanged += CollectionChanges;
            LoadCart();
        }
        private void CollectionChanges(object sender, EventArgs e)
        {
            LoadCart();
        }
        public void LoadCart()
        {
            this.TotalItems = StoreDetailsViewModel.cartDetails.Count;

            double totalprice = 0;
            foreach (ProductList prod in StoreDetailsViewModel.cartDetails)
            {
                double variant_price = 0;
                if (prod.Vrid > 0)
                {
                    variant_price = prod.Vrid_Price;
                }
                totalprice += Convert.ToInt32(prod.Quantity) * (prod.ActualPrice + variant_price);
            }
            this.TotalPrice = totalprice;
            //InsertItemToCart(item.ID, item.Vrid, Convert.ToInt32(item.Quantity), item.ActualPrice);
        }

        public ICommand AddToCartCommand
        {
            get
            {
                return new Command<string>((x) => AddToCartClicked(x));
            }
        }

        private void AddToCartClicked(string id)
        {
            if (Views.Marketplace.ProductDetail.variants_id != 0)
            {
                try
                {
                    TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, productid, "0", "1", Views.Marketplace.ProductDetail.variants_id.ToString(), (bool success, string data) =>
                    {
                        if (success)
                        {
                            Options options = JsonConvert.DeserializeObject<Options>(data);
                            if (options.data.Length > 0)
                            {
                                for (int i = 0; i < options.data.Length; i++)
                                {
                                    StoreDetailsViewModel.InsertCart(productid, Views.Marketplace.ProductDetail.variants_id, Convert.ToDouble(options.data[i].price), this.ProductName, this.Short_Info, this.ProductImage, this.Price, this.Quantity);
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
            else
            {
                {
                    StoreDetailsViewModel.InsertCart(productid, 0, 0, this.ProductName, this.Short_Info, this.ProductImage, this.Price, this.Quantity);
                }
            }
        }

        public void LoadVariants(string product_id)
        {
            try
            {
                Http.TindaFeature.Instance.VariantList_Options(product_id, (bool success, string data) =>
                {
                    if (success)
                    {
                        Variants var = JsonConvert.DeserializeObject<Variants>(data);
                        for (int i = 0; i < var.data.Length; i++)
                        {
                            if (var.data[i].options.Count != 0)
                            {
                                if (var.data[i].baseprice == "Yes")
                                {
                                    _optionsList = new ObservableCollection<Options>();
                                    _optionsList.Clear();
                                    for (int j = 0; j < var.data[i].options.Count; j++)
                                    {
                                        _optionsList.Add(new Options() { Id = var.data[i].options[j].ID, Name = var.data[i].options[j].name, Price = Convert.ToDouble(var.data[i].options[j].price) });
                                    }
                                    _variantsList.Add(new Variants() { Name = var.data[i].name, Base = "Required(1)", options = _optionsList });
                                }
                                else
                                {
                                    _addonsList = new ObservableCollection<Options>();
                                    _addonsList.Clear();
                                    for (int j = 0; j < var.data[i].options.Count; j++)
                                    {
                                        _addonsList.Add(new Options() { Id = var.data[i].options[j].ID, Name = var.data[i].options[j].name, Price = Convert.ToDouble(var.data[i].options[j].price) });
                                    }
                                    _variantsList.Add(new Variants() { Name = var.data[i].name, Base = "Optional", addons = _addonsList });
                                }

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

    }

}
