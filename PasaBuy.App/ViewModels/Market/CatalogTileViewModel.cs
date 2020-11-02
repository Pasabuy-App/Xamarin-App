using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.Market
{
    public class CatalogTileViewModel : BaseViewModel
    {
        public ObservableCollection<ProductList> productList;
        public ObservableCollection<ProductList> Products
        {
            get { return productList; }
            set { productList = value; this.NotifyPropertyChanged(); }
        }
        public CatalogTileViewModel()
        {
            productList = new ObservableCollection<ProductList>();
            LoadData("");

            /*productList.Add(new ProductList()
            {
                PreviewImage = "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png",
                Name = "Example name",
                Summary = "Summary",
                Description = "Description",
                ActualPrice = 220,
                DiscountPercent = 0
            });*/
        }
        public void LoadData(string lastid)
        {
            try
            {
                TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProductListData datas = JsonConvert.DeserializeObject<ProductListData>(data);
                        //Console.WriteLine("dataas" + datas);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string ID = datas.data[i].ID;
                            string preview = datas.data[i].preview == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png" : datas.data[i].preview;
                            string product_name = datas.data[i].product_name;
                            string short_info = datas.data[i].short_info;
                            string long_info = datas.data[i].long_info;
                            double price = datas.data[i].price;
                            productList.Add(new ProductList()
                            {
                                //PreviewImage = "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png",
                                Avatar = PSAProc.GetUrl(preview),
                                Name = product_name,
                                Summary = short_info,
                                Description = long_info,
                                ActualPrice = price,
                                DiscountPercent = 0
                            });
                        }
                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20455.", "OK");
            }
        }
    }
}
