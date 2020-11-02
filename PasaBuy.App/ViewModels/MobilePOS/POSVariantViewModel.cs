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

        public static ObservableCollection<Variants> _variantsList;

        public static ObservableCollection<Options> _optionsList;

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
            _variantsList = new ObservableCollection<Variants>();
            this.Product_Name = product_name;
        }

        public static void LoadVariants(string product_id)
        {
            try
            {
                Http.TindaFeature.Instance.VariantList_Options(product_id, "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Variants var = JsonConvert.DeserializeObject<Variants>(data);

                        for (int i = 0; i < var.data.Length; i++)
                        {
                            if (var.data[i].options.Count != 0)
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
                                    options = _optionsList
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


            /*ObservableCollection<Options> size_options = new ObservableCollection<Options>();
            size_options.Add(new Options() { Name = "Medium", Price = +25.00 });
            size_options.Add(new Options() { Name = "Large", Price = +45.00 });
            size_options.Add(new Options() { Name = "Grande", Price = +65.00 });
            ObservableCollection<Options> sweetness_options = new ObservableCollection<Options>();
            sweetness_options.Add(new Options() { Name = "25%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "50%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "75%", Price = +0.00 });
            sweetness_options.Add(new Options() { Name = "100%", Price = +0.00 });

            _variantsList.Add(new Variants() { Name = "Size", options = size_options });
            _variantsList.Add(new Variants() { Name = "Sweetness Level", options = sweetness_options });*/
        }

        private ICommand _addToOrder;

        public ICommand AddToOrderCommand => _addToOrder ?? (_addToOrder = new Command<string>(AddToOrderClicked));

        private async void AddToOrderClicked(string selectedItemId)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await PopupNavigation.Instance.PopAsync();
                POSViewModel.InsertData(product_id, product_name, product_price, 1);
                await Application.Current.MainPage.Navigation.PopModalAsync();
                IsBusy = false;
            }
        }
    }
}
