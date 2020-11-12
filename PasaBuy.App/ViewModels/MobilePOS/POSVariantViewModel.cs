using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class POSVariantViewModel : BaseViewModel
    {
        public static string product_id;
        public static string product_name;
        public static double product_price;

        public static ObservableCollection<Models.TindaFeature.OptionModel> _addonList;

        public ObservableCollection<Models.TindaFeature.OptionModel> AddonList
        {
            get
            {
                return _addonList;
            }
            set
            {
                _addonList = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Models.TindaFeature.OptionModel> _optionsList;

        public ObservableCollection<Models.TindaFeature.OptionModel> OptionsList
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

        public static ObservableCollection<Models.TindaFeature.VariantModel> _variantsList;

        public ObservableCollection<Models.TindaFeature.VariantModel> VariantsList
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
        public static ObservableCollection<Models.TindaFeature.OptionModel> _OptionsList;
        public ObservableCollection<Models.TindaFeature.OptionModel> OptionList
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
        public static ObservableCollection<Models.TindaFeature.OptionModel> _AddOnsList;
        public ObservableCollection<Models.TindaFeature.OptionModel> AddOnsList
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
        public static ObservableCollection<Models.TindaFeature.OptionModel> _AllVariants;
        public ObservableCollection<Models.TindaFeature.OptionModel> AllVariants
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

        public string _Product_Name = string.Empty;
        public string Product_Name
        {
            get
            {
                return _Product_Name;
            }
            set
            {
                if (_Product_Name == value)
                {
                    return;
                }
                _Product_Name = value;
                this.NotifyPropertyChanged();
            }
        }
        public POSVariantViewModel()
        {
            _variantsList = new ObservableCollection<Models.TindaFeature.VariantModel>();
            _OptionsList = new ObservableCollection<Models.TindaFeature.OptionModel>();
            _AddOnsList = new ObservableCollection<Models.TindaFeature.OptionModel>();
            _AllVariants = new ObservableCollection<Models.TindaFeature.OptionModel>();
            this.Product_Name = product_name;

            LoadVariants();
        }
        public static void InsertVariants(string vrid, string name)
        {
            string option_id = string.Empty;
            double price;
            foreach (Models.TindaFeature.VariantModel var in _variantsList)
            {
                if (var.ID == vrid)
                {
                    foreach (Models.TindaFeature.OptionModel op in var.options)
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
                                _OptionsList.Add(new Models.TindaFeature.OptionModel()
                                {
                                    Vrid = vrid,
                                    Id = option_id,
                                    Name = name,
                                    Price = price
                                });
                            }
                            else
                            {
                                foreach (Models.TindaFeature.OptionModel opt2 in _OptionsList)
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
            foreach (Models.TindaFeature.VariantModel var in _variantsList)
            {
                if (var.ID == vrid)
                {
                    foreach (Models.TindaFeature.OptionModel op in var.addons)
                    {
                        if (op.Vrid == vrid && op.Name == name)
                        {
                            _AddOnsList.Add(new Models.TindaFeature.OptionModel()
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
                foreach (Models.TindaFeature.VariantModel var in _variantsList)
                {
                    if (var.ID == vrid)
                    {
                        foreach (Models.TindaFeature.OptionModel op in var.addons)
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

        public void LoadVariants()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Http.TindaPress.Variant.Instance.Listing(product_id, "", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.VariantModel var = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                            for (int i = 0; i < var.data.Length; i++)
                            {
                                if (var.data[i].options.Count != 0)
                                {
                                    if (var.data[i].required == "true")
                                    {
                                        _optionsList = new ObservableCollection<Models.TindaFeature.OptionModel>();
                                        _optionsList.Clear();
                                        for (int j = 0; j < var.data[i].options.Count; j++)
                                        {
                                            _optionsList.Add(new Models.TindaFeature.OptionModel()
                                            {
                                                Vrid = var.data[i].ID,
                                                Id = var.data[i].options[j].ID,
                                                Name = var.data[i].options[j].name,
                                                Price = Convert.ToDouble(var.data[i].options[j].price)
                                            });
                                        }
                                        _variantsList.Add(new Models.TindaFeature.VariantModel()
                                        {
                                            ID = var.data[i].ID,
                                            Title = var.data[i].title,
                                            Required = "Required(1)",
                                            options = _optionsList
                                        });
                                    }
                                    else
                                    {
                                        _addonList = new ObservableCollection<Models.TindaFeature.OptionModel>();
                                        _addonList.Clear();
                                        for (int j = 0; j < var.data[i].options.Count; j++)
                                        {
                                            _addonList.Add(new Models.TindaFeature.OptionModel()
                                            {
                                                Vrid = var.data[i].ID,
                                                Id = var.data[i].options[j].ID,
                                                Name = var.data[i].options[j].name,
                                                Price = Convert.ToDouble(var.data[i].options[j].price)
                                            });
                                        }
                                        _variantsList.Add(new Models.TindaFeature.VariantModel()
                                        {
                                            ID = var.data[i].ID,
                                            Title = var.data[i].title,
                                            Required = "Optional",
                                            addons = _addonList
                                        });
                                    }
                                    IsBusy = false;
                                }
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsBusy = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L1POS.", "OK");
                IsBusy = false;
            }
        }

        private ICommand _addToOrder;

        public ICommand AddToOrderCommand => _addToOrder ?? (_addToOrder = new Command<string>(AddToOrderClicked));

        private async void AddToOrderClicked(string selectedItemId)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                if (_variantsList.Count > 0)
                {
                    int count = 0;
                    foreach (var item in _variantsList)
                    {
                        if (item.Required == "Required(1)")
                        {
                            count++;
                        }
                    }
                    if (count != _OptionsList.Count) // Count of selected variants/options that required
                    {
                        new Alert("Notice to User", "Please select required options.", "Try Again"); // Show error if required item is not equal to selected
                        IsBusy = false;
                    }
                    else
                    {
                        if (_OptionsList.Count > 0)
                        {
                            foreach (Models.TindaFeature.OptionModel ops in _OptionsList)
                            {
                                _AllVariants.Add(new Models.TindaFeature.OptionModel()
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
                            foreach (Models.TindaFeature.OptionModel ops in _AddOnsList)
                            {
                                _AllVariants.Add(new Models.TindaFeature.OptionModel()
                                {
                                    Vrid = ops.Vrid,
                                    Id = ops.Id,
                                    Name = ops.Name,
                                    Price = ops.Price,
                                });
                            }
                        }

                        POSViewModel.InsertData(product_id, product_name, product_price, 1, _AllVariants);
                        PopupNavigation.Instance.PopAsync();
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        IsBusy = false;
                    }
                }
                else
                {
                    POSViewModel.InsertData(product_id, product_name, product_price, 1, _AllVariants);
                    PopupNavigation.Instance.PopAsync();
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    IsBusy = false;
                }
            }
        }
    }
}
