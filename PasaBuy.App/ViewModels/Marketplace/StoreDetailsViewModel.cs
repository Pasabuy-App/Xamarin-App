
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Syncfusion.XForms.Buttons;
using PasaBuy.App.Models;

using PasaBuy.App.Models.Marketplace;
using Plugin.Media.Abstractions;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;
using PasaBuy.App.Local;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Commands;
using PasaBuy.App.Views.eCommerce;
using System.Diagnostics;

namespace PasaBuy.App.ViewModels.Marketplace
{
    /// <summary>
    /// ViewModel for Article parallax page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class StoreDetailsViewModel : BaseViewModel
    {
        #region Fields

        public static string store_id = string.Empty;

        private static ObservableCollection<StoreDetails> storedetailslist;

        private static ObservableCollection<ProductList> productsList;

        private static ObservableCollection<Categories> categoriesdata;

        private int? cartItemCount;


        #endregion

        public ObservableCollection<StoreDetails> StoreDetailList
        {
            get { return storedetailslist; }
            set { storedetailslist = value; this.NotifyPropertyChanged(); }
        }


        public  ObservableCollection<ProductList> ProductsList
        {
            get { return productsList; }
            set { productsList = value; this.NotifyPropertyChanged(); }
        }


        public ObservableCollection<Categories> Categoriesdata
        {
            get { return categoriesdata; }
            set { categoriesdata = value; this.NotifyPropertyChanged(); }
        }

        public int? CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        

        public StoreDetailsViewModel()
        {
            this.AddToCartCommand = new Command(this.AddToCartClicked);
            this.GoToCartCommand = new Command(this.GoToCartClicked);

            //loadstoredetails(store_id);
            loadstoredetails();

        }

        public Command AddToCartCommand { get; set; }

        public Command GoToCartCommand { get; set; }

        private void AddToCartClicked(object obj)
        {
            this.cartItemCount = this.cartItemCount ?? 0;
            this.CartItemCount += 1;
        }
      
        private async void GoToCartClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartPage());
        }

        public static void loaddata(string stid)
        {
            loadcategory(stid);
            //loadproduct(stid, Id);
        }

        public static void loadstoredetails()
        {
            try
            {
                storedetailslist = new ObservableCollection<StoreDetails>();
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", store_id, "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreDetailListData datas = JsonConvert.DeserializeObject<StoreDetailListData>(data);
                        for(int i = 0; i < datas.data.Length; i++)
                        {
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : datas.data[i].banner;
                            string longinfo = datas.data[i].long_info;
                            string city = datas.data[i].city;
                            storedetailslist.Add(new StoreDetails() { 
                                StoreTitle = title,
                                StoreDescription = short_info,
                                Logo = PSAProc.GetUrl(avatar),
                                Banner = banner,
                                LongInformation = longinfo,
                                City = city

                            });;
                        }
                    }
                    else
                    {
                        new Alert("Something went wrong!", "Please contact your administratir for this issue. Error code 404", "");
                    }
                });
            }
            catch(Exception ex)
            {
                new Alert("Something went wrong!", "Please contact your administratir for this issue. Error code 404"+ ex, "");
            }
        }
        
        
        public static void loadcategory(string stid)
        {
            try
            {
                categoriesdata = new ObservableCollection<Categories>();

                TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", stid, "", "1", (bool success, string data) => 
                {
                    if (success)
                    {
                        productsList = new ObservableCollection<ProductList>();
                        TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, stid, "", "", "1", "", (bool success1, string data1) => {
                            try
                            {
                                if (success1)
                                {
                                    ProductListData productData = JsonConvert.DeserializeObject<ProductListData>(data1);
                                    
                                    for (int y = 0; y < productData.data.Length; y++ )
                                    {
                                        string product_name = productData.data[y].product_name;
                                        string short_info = productData.data[y].short_info;
                                        double price = productData.data[y].price;
                                        productsList.Add(new ProductList()
                                        {
                                            Name = product_name,
                                            description = short_info,
                                            actualprice = price
                                        });
                                        
                                    }
                                    
                                }
                                
                            }
                            catch (Exception e)
                            {
                                new Alert("Something went wrong!", "Please contact your administrator for this issue. Error code 404", "");
                            }
                        });

                        CategoriesListData datas = JsonConvert.DeserializeObject<CategoriesListData>(data);
                        for(int i = 0; i < datas.data.Length; i++)
                        {
                            
                            //string id = datas.data[i].ID;
                            string title = datas.data[i].title;
                            categoriesdata.Add(new Categories()
                            {
                                Title = title, 
                                Prods = productsList

                            });

                        }

                       
                    }
                    else
                    {
                        new Alert("Something went wrong!", "Please contact your administratir for this issue. Error code 404", "");
                    }
                });
                

            }
            catch (Exception e)
            {

            }
        }



       


    }
}