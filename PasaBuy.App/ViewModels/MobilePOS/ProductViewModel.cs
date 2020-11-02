using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.StoreViews;
using PasaBuy.App.Views.StoreViews.Management;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class ProductViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<Models.TindaFeature.ProductModel> productsList;

        public ICommand ShowVariantsCommand
        {
            get
            {
                return new Command<string>((x) => ShowVariantsClicked(x));
            }
        }
        public bool isClicked = false;
        private async void ShowVariantsClicked(string id)
        {
            if (!isClicked)
            {
                isClicked = true;
                await Application.Current.MainPage.Navigation.PushModalAsync(new ProductVariants()); ;
                VariantsViewModel.LoadVariants(id);
                ProductVariants.product_id = id;
                await Task.Delay(200);
                isClicked = false;
            }
        }

        public ObservableCollection<Models.TindaFeature.ProductModel> ProductsList
        {
            get 
            { 
                return productsList; 
            }
            set 
            { 
                if (productsList != value)
                {
                    productsList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool isRunning = false;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }

        #endregion
        public ProductViewModel()
        {
            productsList = new ObservableCollection<Models.TindaFeature.ProductModel>();
            LoadProduct();
            UpdateCommand = new Command<object>(UpdateClicked);
            AddCommand = new Command<object>(AddClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                LoadProduct();
                IsRefreshing = false;
            });
        }
        public Command<object> AddCommand { get; set; }

        private async void AddClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                AddProductView.pdid = "0";
                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddProductView()));
                IsRunning = false;
            }
        }

        public Command<object> UpdateCommand { get; set; }

        private async void UpdateClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var product = obj as Models.TindaFeature.ProductModel;
                AddProductView.pdid = product.ID;
                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddProductView()));
                IsRunning = false;
            }
        }

        public void LoadProduct()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    productsList.Clear();
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
                                    Discount = product.data[i].discount,
                                    Category_Name = product.data[i].category_name,
                                    Preview = PSAProc.GetUrl(product.data[i].avatar)
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L1PVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshProduct(string search)
        {
            try
            {
                productsList.Clear();
                Http.TindaPress.Product.Instance.Listing("", search, "", (bool success, string data) =>
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
                                Discount = product.data[i].discount,
                                Category_Name = product.data[i].category_name,
                                Preview = PSAProc.GetUrl(product.data[i].avatar)
                            });
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L2PVM.", "OK");
            }
        }
    }
}