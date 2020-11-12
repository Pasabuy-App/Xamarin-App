using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.PopupModals;
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
    public class POSProductViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.TindaFeature.ProductModel> productsList;
        public ObservableCollection<Models.TindaFeature.ProductModel> ProductsList
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
        public POSProductViewModel()
        {
            productsList = new ObservableCollection<Models.TindaFeature.ProductModel>();
            _AllVariants = new ObservableCollection<Models.TindaFeature.OptionModel>();
            LoadProduct();
        }
        private void LoadProduct()
        {
            try
            {
                IsBusy = true;
                Http.TindaPress.Product.Instance.Listing("", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.ProductModel product = JsonConvert.DeserializeObject<Models.TindaFeature.ProductModel>(data);
                        for (int i = 0; i < product.data.Length; i++)
                        {
                            productsList.Add(new Models.TindaFeature.ProductModel()
                            {
                                ID = product.data[i].ID,
                                Product_name = product.data[i].title,
                                Short_info = product.data[i].info,
                                Price = Convert.ToDouble(product.data[i].price),
                                Preview = PSAProc.GetUrl(product.data[i].avatar)
                            });
                        }
                        IsBusy = false;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        IsBusy = false;
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L1POS.", "OK");
                IsBusy = false;
            }
        }

        private ICommand _showVariants;
        public ICommand ShowVariantsCommand => _showVariants ?? (_showVariants = new Command<string>(ShowVariantsClicked));
        private void ShowVariantsClicked(string selectedItemId)
        {
            var item = ProductsList.FirstOrDefault(x => x.ID.Equals(selectedItemId));
            CheckVariant(item.ID, item.Product_name, item.Price);
        }
        public void CheckVariant(string product_id, string product_name, double product_price)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;

                    Http.TindaPress.Variant.Instance.Listing(product_id, "", "", "active", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.VariantModel variants = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                            if (variants.data.Length > 0)
                            {
                                for (int i = 0; i < variants.data.Length; i++)
                                {
                                    if (variants.data[i].options.Count != 0)
                                    {
                                        POSVariantViewModel.product_id = product_id;
                                        POSVariantViewModel.product_name = product_name;
                                        POSVariantViewModel.product_price = product_price;
                                        await PopupNavigation.Instance.PushAsync(new PopupShowVariants());
                                        IsBusy = false;
                                        break;
                                    }
                                    else
                                    {
                                        POSViewModel.InsertData(product_id, product_name, product_price, 1, _AllVariants);
                                        await Application.Current.MainPage.Navigation.PopModalAsync();
                                        IsBusy = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                POSViewModel.InsertData(product_id, product_name, product_price, 1, _AllVariants);
                                await Application.Current.MainPage.Navigation.PopModalAsync();
                                IsBusy = false;
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L2POS.", "OK");
                IsBusy = false;
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
    }
}
