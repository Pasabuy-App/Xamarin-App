using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
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
        public static ObservableCollection<Options> _AllVariants;
        public ObservableCollection<Options> AllVariants
        {
            get
            {
                return _AllVariants;
            }
            set
            {
                _AllVariants = value;
                this.NotifyPropertyChanged();
            }
        }
        public static ObservableCollection<Options> _OptionsList;
        public ObservableCollection<Options> OptionList
        {
            get
            {
                return _OptionsList;
            }
            set
            {
                _OptionsList = value;
                this.NotifyPropertyChanged();
            }
        }
        public static ObservableCollection<Options> _AddOnsList;
        public ObservableCollection<Options> AddOnsList
        {
            get
            {
                return _AddOnsList;
            }
            set
            {
                _AddOnsList = value;
                this.NotifyPropertyChanged();
            }
        }
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

        public static ObservableCollection<Options> _optionsList;
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
        public static ObservableCollection<Variants> _variantsList;

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
        public static double price;
        public static double discount;
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
        public ProductDetailViewModel()
        {
            _itemList = new ObservableCollection<ProductList>();
            _variantsList = new ObservableCollection<Variants>();

            _optionsList = new ObservableCollection<Options>();
            _addonsList = new ObservableCollection<Options>();
            _AddOnsList = new ObservableCollection<Options>();
            _OptionsList = new ObservableCollection<Options>();
            _AllVariants = new ObservableCollection<Options>();

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

        public static void InsertVariants(string vrid, string name)
        {
            string option_id = string.Empty;
            double price;
            foreach (Variants var in _variantsList)
            {
                if (var.ID == vrid)
                {
                    foreach (Options op in var.options)
                    {
                        if (op.Vrid == vrid && op.Name == name)
                        {
                            option_id = op.Id;
                            price = op.Price;
                            bool itemExists = _OptionsList.Any(ops =>
                            {
                                return (ops.Vrid == vrid);
                            });
                            if (!itemExists)
                            {
                                _OptionsList.Add(new Options()
                                {
                                    Vrid = vrid,
                                    Id = option_id,
                                    Name = name,
                                    Price = price
                                });
                            }
                            else
                            {
                                foreach (Options opt2 in _OptionsList)
                                {
                                    if (opt2.Vrid == vrid)
                                    {
                                        opt2.Id = option_id;
                                        opt2.Name = name;
                                        opt2.Price = price;
                                        break;
                                    }
                                }
                            }
                            break;
                        }

                    }
                    break;
                }
            }
        }
        public static void InsertAddOns(string vrid, string name)
        {
            foreach (Variants var in _variantsList)
            {
                if (var.ID == vrid)
                {
                    foreach (Options op in var.addons)
                    {
                        if (op.Vrid == vrid && op.Name == name)
                        {
                            _AddOnsList.Add(new Options()
                            {
                                Vrid = vrid,
                                Id = op.Id,
                                Name = name,
                                Price = op.Price
                            });
                            break;
                        }

                    }
                    break;
                }
            }
        }
        public static void RemoveAddOns(string vrid, string name)
        {
            if (_AddOnsList.Count > 0)
            {
                foreach (Variants var in _variantsList)
                {
                    if (var.ID == vrid)
                    {
                        foreach (Options op in var.addons)
                        {
                            if (op.Vrid == vrid && op.Name == name)
                            {
                                foreach (var item in _AddOnsList)
                                {
                                    if (item.Vrid == vrid && item.Id == op.Id && item.Name == name)
                                    {
                                        _AddOnsList.Remove(item);
                                        break;
                                    }
                                }
                                break;
                            }

                        }
                        break;
                    }
                }
            }
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
                double varprice = 0;
                foreach (Options var in prod.Variants)
                {
                    varprice += var.Price;
                }
                totalprice += prod.Quantity * (prod.ActualPrice + varprice);
            }
            this.TotalPrice = totalprice;
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
            if (!isRunning)
            {
                isRunning = true;
                if (string.IsNullOrEmpty(StoreDetailsViewModel.operation_id))
                {
                    new Alert("Notice to User", "This store is closed. You can't order right now.", "OK");
                    isRunning = false;
                    return;
                }
                if (_variantsList.Count > 0)
                {
                    int count = 0;
                    foreach (var item in _variantsList)
                    {
                        if (item.Base == "Required(1)")
                        {
                            count++;
                        }
                    }
                    if (count != _OptionsList.Count) // Count of selected variants/options that required
                    {
                        new Alert("Notice to User", "Please select required options.", "Try Again"); // Show error if required item is not equal to selected
                        isRunning = false;
                    }
                    else
                    {
                        if (_OptionsList.Count > 0)
                        {
                            foreach (Options ops in _OptionsList)
                            {
                                _AllVariants.Add(new Options()
                                {
                                    Vrid = ops.Vrid,
                                    Id = ops.Id,
                                    Name = ops.Name,
                                    Price = ops.Price,
                                });
                            }
                        }
                        if (_AddOnsList.Count > 0)
                        {
                            foreach (Options ops in _AddOnsList)
                            {
                                _AllVariants.Add(new Options()
                                {
                                    Vrid = ops.Vrid,
                                    Id = ops.Id,
                                    Name = ops.Name,
                                    Price = ops.Price,
                                });
                            }
                        }

                        StoreDetailsViewModel.InsertCart(productid, this.ProductName, this.Short_Info, this.ProductImage, this.Price, discount, this.SpecialInstructions, this.Quantity, _AllVariants);
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        isRunning = false;
                    }
                }
                else
                {
                    StoreDetailsViewModel.InsertCart(productid, this.ProductName, this.Short_Info, this.ProductImage, this.Price, discount, this.SpecialInstructions, this.Quantity, _AllVariants);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    isRunning = false;
                }
            }
        }

        public void LoadVariants(string product_id)
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    Http.TindaPress.Variant.Instance.Listing(product_id, "", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Variants var = JsonConvert.DeserializeObject<Variants>(data);
                            if (var.data.Length == 0)
                            {
                                isRunning = false;
                            }
                            for (int i = 0; i < var.data.Length; i++)
                            {
                                if (var.data[i].options.Count != 0)
                                {
                                    if (var.data[i].required == "true")
                                    {
                                        _optionsList = new ObservableCollection<Options>();
                                        _optionsList.Clear();
                                        for (int j = 0; j < var.data[i].options.Count; j++)
                                        {
                                            _optionsList.Add(new Options()
                                            {
                                                Vrid = var.data[i].ID,
                                                Id = var.data[i].options[j].ID,
                                                Name = var.data[i].options[j].name,
                                                Price = Convert.ToDouble(var.data[i].options[j].price)
                                            });
                                        }
                                        _variantsList.Add(new Variants()
                                        {
                                            ID = var.data[i].ID,
                                            Title = var.data[i].title,
                                            Base = "Required(1)",
                                            options = _optionsList
                                        });
                                    }
                                    else
                                    {
                                        _addonsList = new ObservableCollection<Options>();
                                        _addonsList.Clear();
                                        for (int j = 0; j < var.data[i].options.Count; j++)
                                        {
                                            _addonsList.Add(new Options()
                                            {
                                                Vrid = var.data[i].ID,
                                                Id = var.data[i].options[j].ID,
                                                Name = var.data[i].options[j].name,
                                                Price = Convert.ToDouble(var.data[i].options[j].price)
                                            });
                                        }
                                        _variantsList.Add(new Variants()
                                        {
                                            ID = var.data[i].ID,
                                            Title = var.data[i].title,
                                            Base = "Optional",
                                            addons = _addonsList
                                        });
                                    }
                                    isRunning = false;
                                }
                                else
                                {
                                    isRunning = false;
                                }
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            isRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L1PDVM.", "OK");
            }
        }

    }

}
