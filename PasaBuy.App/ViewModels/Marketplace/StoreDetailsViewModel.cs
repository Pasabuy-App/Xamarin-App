
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

        private static ObservableCollection<Product> producdetailstlist;

        private static ObservableCollection<Categories> categoriesdata;

        private int? cartItemCount;


        #endregion

        public ObservableCollection<StoreDetails> StoreDetailList
        {
            get { return storedetailslist; }
            set { storedetailslist = value; this.NotifyPropertyChanged(); }
        }


        public  ObservableCollection<Product> Producdetailstlist
        {
            get { return producdetailstlist; }
            set { producdetailstlist = value; this.NotifyPropertyChanged(); }
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
                        CategoriesListData datas = JsonConvert.DeserializeObject<CategoriesListData>(data);
                        for(int i = 0; i < datas.data.Length; i++)
                        {
                            //string id = datas.data[i].ID;
                            string title = datas.data[i].title;
                            categoriesdata.Add(new Categories()
                            {
                                Title = title, 
                                Prods = new ObservableCollection<ProductList>() 
                                { 
                                    new ProductList() 
                                    {
                                        Name = "Cheese Burst", actualprice = 300, description = "Burger topped with cheese and patty"
                                    }, 
                                    new ProductList() 
                                    {
                                        Name = "Fresh Pan Pizza", actualprice = 310, description = "Thick pizza baked in a deep dish pan"
                                    }, 
                                    new ProductList() 
                                    { 
                                        Name = "All Meat Pizza", actualprice = 480 , description = "Homemade thin crust pizza, topped off with two types of cheese, bacon, ham, pepperoni and hot sausage"
                                    } 
                                } 
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

        public static void loadproduct()
        {
            try
            {
                producdetailstlist = new ObservableCollection<Product>();

                producdetailstlist.Add(new Product()
                {
                    Name = "catid",
                    Description = store_id
                });

                producdetailstlist.Add(new Product()
                {
                    Name = "catid",
                    Description = "The desciption 2."
                });
                producdetailstlist.Add(new Product()
                {
                    Name = "catid",
                    Description = "The desciption 2."
                });
                producdetailstlist.Add(new Product()
                {
                    Name = "catid",
                    Description = "The desciption 2."
                });

            }
            catch (Exception e)
            {

            }
        }

       


    }
}