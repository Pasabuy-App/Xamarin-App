using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class ProductViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<ProductData> productsList;
        public ObservableCollection<ProductData> ProductsList
        {
            get { return productsList; }
            set { productsList = value; this.NotifyPropertyChanged(); }
        }
        #endregion
        public ProductViewModel()
        {
            productsList = new ObservableCollection<ProductData>();
            productsList.Clear();
            LoadData("");
            /*productsList.Add(new ProductData()
            {
                ID = "0",
                Product_name = "Name",
                Short_info = "Info",
                Price = "100.00",
                Preview = ""
            });*/
        }
        public static void LoadData(string lastid)
        {
            try
            {
                TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "1", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProductData product = JsonConvert.DeserializeObject<ProductData>(data);
                        for (int i = 0; i < product.data.Length; i++)
                        {
                            string id = product.data[i].ID;
                            string product_name = product.data[i].product_name;
                            string short_info = product.data[i].short_info;
                            string price = product.data[i].price;
                            string preview = product.data[i].preview;
                            productsList.Add(new ProductData()
                            {
                                ID = id,
                                Product_name = product_name,
                                Short_info = short_info,
                                Price = price,
                                Preview = PSAProc.GetUrl(preview)
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
            }
        }
    }
}
