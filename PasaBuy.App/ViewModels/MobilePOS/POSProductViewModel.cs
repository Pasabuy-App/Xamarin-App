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
            LoadProduct();
        }
        private void LoadProduct()
        {
            try
            {
                IsBusy = true;
                TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "1", "", (bool success, string data) =>
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }

        private ICommand _showVariants;
        public ICommand ShowVariantsCommand => _showVariants ?? (_showVariants = new Command<string>(ShowVariantsClicked));
        private void ShowVariantsClicked(string selectedItemId)
        {
            var item = ProductsList.FirstOrDefault(x => x.ID.Equals(selectedItemId));
            ChechVariant(item.ID, item.Product_name, item.Price);
        }
        public void ChechVariant(string product_id, string product_name, double product_price)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Http.TindaFeature.Instance.VariantList_Options(product_id, "active", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            Variants var = JsonConvert.DeserializeObject<Variants>(data);
                            if (var.data.Length > 0)
                            {
                                POSVariantViewModel.product_id = product_id;
                                POSVariantViewModel.product_name = product_name;
                                POSVariantViewModel.product_price = product_price;
                                POSVariantViewModel.LoadVariants(product_id);
                                await PopupNavigation.Instance.PushAsync(new PopupShowVariants());
                                IsBusy = false;
                            }
                            else
                            {
                                POSViewModel.InsertData(product_id, product_name, product_price, 1);
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }
    }
}
