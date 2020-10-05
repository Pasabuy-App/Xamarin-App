
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Syncfusion.XForms.Buttons;
using PasaBuy.App.Models;

using PasaBuy.App.Models.Marketplace;
using Plugin.Media.Abstractions;
using Newtonsoft.Json;
using System;
using PasaBuy.App.Local;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Commands;
using PasaBuy.App.Views.eCommerce;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using PasaBuy.App.Views.ErrorAndEmpty;
using PasaBuy.App.ViewModels.eCommerce;
using Xamarin.Essentials;

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

        private int cartItemCount = 0;

        private Boolean isCartBusy;
        public bool isCartClicked = false;

        private bool _isAdded = true;

        private bool _notAdded = false;
        public bool IsAdded
        {
            get
            {
                return this._isAdded;
            }
            set
            {
                this._isAdded = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool NotAdded
        {
            get
            {
                return this._notAdded;
            }
            set
            {
                this._notAdded = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<StoreDetails> StoreDetailList
        {
            get 
            { 
                return storedetailslist; 
            }
            set 
            { 
                storedetailslist = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public  ObservableCollection<ProductList> ProductsList
        {
            get 
            { 
                return productsList; }
            set 
            { 
                productsList = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public ObservableCollection<Categories> Categoriesdata
        {
            get 
            { 
                return categoriesdata; 
            }
            set 
            { 
                categoriesdata = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public int CartItemCount
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

        public Boolean IsCartBusy
        {
            get
            {
                return this.isCartBusy;
            }
            set
            {
                this.isCartBusy = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion
        public StoreDetailsViewModel()
        {
            CartPageViewModel.cartDetails = new ObservableCollection<ProductList>();
            this.AddToCartCommand = new Command(this.AddToCartClicked);
            //this.AddToCartCommand = new Command(this.AddToCartClicked);
            storedetailslist = new ObservableCollection<StoreDetails>();
            storedetailslist.Clear();
            categoriesdata = new ObservableCollection<Categories>();
            CartPageViewModel.cartDetails.CollectionChanged += CollectionChanges;
            categoriesdata.Clear();
            //loadstoredetails(store_id);
            //loadstoredetails("");
        }
        private void CollectionChanges(object sender, EventArgs e)
        {
            this.CartItemCount = CartPageViewModel.cartDetails.Count;
        }
        public Command AddToCartCommand { get; set; }

        //public Command GoToCartCommand { get; set; }

        ICommand goToCartCommand;
        public ICommand GoToCartCommand
        {
            get
            {
                if (goToCartCommand == null)
                    goToCartCommand = new Command(GoToCartClicked, (x) => CanNavigate);

                return goToCartCommand;
            }
        }
        private async void AddToCartClicked(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.ProductDetail());
             try
             {
                 if (!isCartClicked)
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.ProductDetail());
                    isCartClicked = true;
                    var btn = obj as SfButton;
                    //btn.IsVisible = false;
                    //btn.BackgroundImage = "Idcard.png";
                    //btn.BackgroundColor = Color.Red;
                    //btn.Text = "&#xe724;";
                    //this.cartItemCount = this.cartItemCount;
                    //this.CartItemCount += 1;
                    TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, store_id, "", btn.ClassId, "1", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            ProductListData datas = JsonConvert.DeserializeObject<ProductListData>(data);
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                Views.Marketplace.ProductDetail.productid = datas.data[i].ID.ToString();
                                Views.Marketplace.ProductDetail.productname = datas.data[i].product_name.ToString();
                                Views.Marketplace.ProductDetail.shortinfo = datas.data[i].short_info.ToString();

                                Views.Marketplace.ProductDetail.productimage = PSAProc.GetUrl(datas.data[i].preview.ToString());
                                Views.Marketplace.ProductDetail.price = datas.data[i].price.ToString();
                                Views.Marketplace.ProductDetail.totalprice = datas.data[i].price.ToString();
                                //CartPageViewModel.InsertCart(store_id, datas.data[i].ID, datas.data[i].product_name, datas.data[i].short_info, datas.data[i].preview, Convert.ToDouble(datas.data[i].price), Convert.ToDouble(datas.data[i].price), 1);
                            }
                            Application.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.ProductDetail());
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                    await Task.Delay(500);
                    isCartClicked = false;
                 }
             }
             catch (Exception e)
             {
                 new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
             }
        }
      
        async void GoToCartClicked(object obj)
        {
                if (!isCartClicked)
                {
                    if (this.cartItemCount != 0)
                    {
                        isCartClicked = true;
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new CartPage());
                        CanNavigate = false;
                        IsCartBusy = true;
                        //await RunTask(NavigateToPage(new CartPage()));
                        await NavigateToPage(new CartPage());
                        CanNavigate = true;
                        IsCartBusy = false;
                        await Task.Delay(100);
                        isCartClicked = false;
                    }
                    else
                    {
                        isCartClicked = true;
                        CanNavigate = false;
                        IsCartBusy = true;
                        await NavigateToPage(new EmptyCartPage());
                        CanNavigate = true;
                        IsCartBusy = false;
                        await Task.Delay(100);
                        isCartClicked = false;
                    }
                }

        }

        async Task NavigateToPage(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public static void loaddata(string stid)
        {
            //loadcategory(stid);
            //loadproduct(stid, Id);
        }

        public static void loadstoredetails(string stid)
        {
            try
            {
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", stid, "1", "", (bool success, string data) =>
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
                            string province = datas.data[i].province;
                            string city = datas.data[i].city;
                            storedetailslist.Add(new StoreDetails() { 
                                StoreTitle = title,
                                StoreDescription = short_info,
                                Logo = PSAProc.GetUrl(avatar),
                                Banner = banner,
                                Province = city + ", " + province,
                                City = city

                            });;
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
        }
        
        public static void loadcategory(string stid)
        {
            try
            {
                TindaPress.Category.Instance.ProductList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", stid, "", "1", (bool success, string data) => { 
                    if(success)
                    {
                        //Console.WriteLine("data: " + data);
                        //CategoryProductList myDeserializedClass = JsonConvert.DeserializeObject<CategoryProductList>(data);
                        Root catRoot = JsonConvert.DeserializeObject<Root>(data);
                        for (int i = 0; i < catRoot.data.Length; i++)
                        {
                            /*for (int ii = 0; ii < catRoot.data[i].products.Count; ii++ )
                            {
                                productsList.Add(new ProductList()
                                {
                                    Name = catRoot.data[i].products[ii].product_name,
                                    ActualPrice = 123, // Convert.ToDouble(catRoot.data[i].products[ii].price)
                                    Description = catRoot.data[i].products[ii].short_info
                                });
                            }*/
                            //Console.WriteLine(catRoot.data[i].title + " Product Count: " + catRoot.data[i].products.Count);
                            if (catRoot.data[i].products.Count != 0)
                            {
                                productsList = new ObservableCollection<ProductList>();
                                productsList.Clear();
                                for (int j = 0; j < catRoot.data[i].products.Count; j++)
                                {
                                    //Console.WriteLine("Product Price: " + catRoot.data[i].products[j].price);
                                    productsList.Add(new ProductList()
                                    {
                                        ID = catRoot.data[i].products[j].ID,
                                        PreviewImage = PSAProc.GetUrl(catRoot.data[i].products[j].preview) == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png" : PSAProc.GetUrl(catRoot.data[i].products[j].preview),
                                        /*Name = catRoot.data[i].products[j].product_name,
                                        ActualPrice = Convert.ToDouble(catRoot.data[i].products[j].price),
                                        Description = catRoot.data[i].products[j].short_info*/
                                        Name = catRoot.data[i].products[j].product_name, // "Name: " + i + " " + j,
                                        ActualPrice = Convert.ToDouble(catRoot.data[i].products[j].price),
                                        Description = catRoot.data[i].products[j].short_info //"Info: " + i + " " + j
                                    });
                                }
                                /*productsList.Add(new ProductList()
                                {
                                    *//*Name = catRoot.data[i].products[1].product_name,
                                    ActualPrice = Convert.ToDouble(catRoot.data[i].products[1].price),
                                    Description = catRoot.data[i].products[1].product_name*/
                                    /*Name = "Burger",
                                    ActualPrice = 123,
                                    Description = "Just another description"*//*
                                    Name = catRoot.data[i].products[0].product_name,
                                    ActualPrice = Convert.ToDouble(catRoot.data[i].products[0].price),
                                    Description = catRoot.data[i].products[0].short_info
                                });*/
                                categoriesdata.Add(new Categories()
                                {
                                    Id = catRoot.data[i].ID,
                                    Title = catRoot.data[i].title,
                                    Prods = productsList
                                });
                            }
                            /*productsList.Add(new ProductList()
                            {
                                *//*Name = catRoot.data[i].products[1].product_name,
                                ActualPrice = Convert.ToDouble(catRoot.data[i].products[1].price),
                                Description = catRoot.data[i].products[1].product_name*/
                                /*Name = "Burger",
                                ActualPrice = 123,
                                Description = "Just another description"*//*
                                Name = catRoot.data[0].products[1].product_name,
                                ActualPrice = Convert.ToDouble(catRoot.data[0].products[1].price),
                                Description = catRoot.data[0].products[1].product_name
                            });*/
                            /*productsList.Add(new ProductList()
                            {
                                Name = catRoot.data[i].products[1].product_name,
                                ActualPrice = Convert.ToDouble(catRoot.data[i].products[1].price),
                                Description = catRoot.data[i].products[1].product_name
                            });*/
                            //Console.WriteLine("ID: " + catRoot.data[i].ID + " Product ID:" + catRoot.data[i].products[i].ID);
                        } 
                        /*foreach (var obj in catRoot.products)
                        {
                            Console.WriteLine("ID: " + obj.ID);
                        }*/
                        /*var jsonObj = new Nancy.Json.JavaScriptSerializer().Deserialize<CatRoot>(data);
                        foreach (var obj in jsonObj.products)
                        {
                            Console.WriteLine("ID: " + obj.ID);
                        }*/

                        /*dynamic jsonObj = JsonConvert.DeserializeObject(data);

                        foreach (var obj in jsonObj.objectList)
                        {
                            Console.WriteLine(obj.ID);
                        }*/


                        //CategoriesListData datas = JsonConvert.DeserializeObject<CategoriesListData>(data);
                        /*string title = string.Empty;
                        string product_name = string.Empty;
                        double actual_price = 0;

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            title = datas.data[i].title;
                            productsList = new ObservableCollection<ProductList>();

                                productsList.Add(new ProductList()
                                {
                                    Name = "Burger",
                                    ActualPrice = 123,
                                    Description = "Just another description"
                                });
                                productsList.Add(new ProductList()
                                {
                                    Name = "Burger",
                                    ActualPrice = 123,
                                    Description = "Just another description"
                                });

                            categoriesdata.Add(new Categories()
                            {
                                Title = title,
                                Prods = productsList
                            });

                        }*/

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

    }
}