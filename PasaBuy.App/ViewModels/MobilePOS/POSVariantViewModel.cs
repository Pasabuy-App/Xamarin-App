using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.Product_Name = product_name;

            LoadVariants();
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
                                            Required = "Required: Yes",
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
                                            Required = "Required: No",
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
                POSViewModel.InsertData(product_id, product_name, product_price, 1);
                PopupNavigation.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PopModalAsync();
                IsBusy = false;
            }
        }
    }
}
