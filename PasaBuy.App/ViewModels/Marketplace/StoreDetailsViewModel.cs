
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


        private Command<object> itemTappedCommand;

        /* store details observable collection */
        private static ObservableCollection<StoreDetails> storedetailslist;

        public  ObservableCollection<StoreDetails> StoreDetailList
        {
            get { return storedetailslist; }
            set { storedetailslist = value; this.NotifyPropertyChanged(); }
        }
        /* end store details observable collection */


        /* product details observable collection */
         private static ObservableCollection<Product> producdetailstlist;

        public  ObservableCollection<Product> Producdetailstlist
        {
            get { return producdetailstlist; }
            set { producdetailstlist = value; this.NotifyPropertyChanged(); }
        }

        /* end product details observable collection */


        /* product details observable collection */
        private static ObservableCollection<Categories> categoriesdata;

        public ObservableCollection<Categories> Categoriesdata
        {
            get { return categoriesdata; }
            set { categoriesdata = value; this.NotifyPropertyChanged(); }
        }

        /* end product details observable collection */





        public StoreDetailsViewModel()
        {
            //loadstoredetails(store_id);
            loadstoredetails();
            
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
                     
                            }) ;

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



        #endregion
    }
}