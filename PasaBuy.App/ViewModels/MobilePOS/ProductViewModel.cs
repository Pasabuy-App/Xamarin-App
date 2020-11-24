using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.StoreViews;
using PasaBuy.App.Views.StoreViews.Management;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class ProductViewModel : BaseViewModel
    {
        #region Fields

        public static ObservableCollection<Models.TindaFeature.ProductModel> productsList;

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
            this.ProductsList = new ObservableCollection<Models.TindaFeature.ProductModel>();
            LoadProduct();

            productsList = new ObservableCollection<Models.TindaFeature.ProductModel>();

            DeleteCommand = new Command<object>(DeleteClicked);
            UpdateCommand = new Command<object>(UpdateClicked);
            AddCommand = new Command<object>(AddClicked);
            ShowVariantsCommand = new Command<object>(VariantClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                LoadProduct();
                IsRefreshing = false;
            });
        }
        public Command<object> ShowVariantsCommand { get; set; }

        private async void VariantClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var product = obj as Models.TindaFeature.ProductModel;
                VariantsViewModel.product_id = product.ID;
                ProductVariants.product_id = product.ID;
                ProductVariants.product_name = product.Product_name;
                await Application.Current.MainPage.Navigation.PushModalAsync(new ProductVariants());
                IsRunning = false;
            }
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
                AddProductView.pcid = product.Category_ID;
                AddProductView.pdid = product.ID;
                AddProductView.name = product.Product_name;
                AddProductView.info = product.Short_info;
                AddProductView.price = product.Price.ToString();
                AddProductView.discount = product.Discount;
                AddProductView.avatar = product.Preview;
                AddProductView.category_name = product.Category_Name;
                AddProductView.inventory = product.Inventory;
                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddProductView()));
                IsRunning = false;
            }
        }

        public Command<object> DeleteCommand { get; set; }

        private async void DeleteClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Product?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var product = obj as Models.TindaFeature.ProductModel;
                        Http.TindaPress.Product.Instance.Delete(product.ID, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RefreshProduct("");
                                IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning = false;
                            }
                        });
                    }
                    else
                    {
                        IsRunning = false;
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2PDT-D1PVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2PDT-D1PVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-D1PVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2PDT-D1PVM-" + err.ToString());
                }
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
                    this.ProductsList.Clear();
                    Http.TindaPress.Product.Instance.Listing("", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.ProductModel product = JsonConvert.DeserializeObject<Models.TindaFeature.ProductModel>(data);
                            for (int i = 0; i < product.data.Length; i++)
                            {
                                bool update = false;
                                bool delete = false;
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_products"))
                                {
                                    update = true;
                                }
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_products"))
                                {
                                    delete = true;
                                }
                                this.ProductsList.Add(new Models.TindaFeature.ProductModel()
                                {
                                    ID = product.data[i].ID,
                                    Product_name = product.data[i].title,
                                    Short_info = product.data[i].info,
                                    Price = Convert.ToDouble(product.data[i].price),
                                    Discount = product.data[i].discount,
                                    Category_Name = product.data[i].category_name,
                                    Category_ID = product.data[i].pcid,
                                    Inventory = product.data[i].inventory,
                                    Preview = PSAProc.GetUrl(product.data[i].avatar),
                                    isUpdate = update,
                                    isDelete = delete,
                                    isDeleteCol = update == true ? 1 : 0
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2PDT-L1PVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2PDT-L1PVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L1PVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2PDT-L1PVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

        public static void RefreshProduct(string search)
        {
            try
            {
                productsList.Clear();
                Http.TindaPress.Product.Instance.Listing("", search, "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.ProductModel product = JsonConvert.DeserializeObject<Models.TindaFeature.ProductModel>(data);
                        for (int i = 0; i < product.data.Length; i++)
                        {
                            bool update = false;
                            bool delete = false;
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_products"))
                            {
                                update = true;
                            }
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_products"))
                            {
                                delete = true;
                            }
                            productsList.Add(new Models.TindaFeature.ProductModel()
                            {
                                ID = product.data[i].ID,
                                Product_name = product.data[i].title,
                                Short_info = product.data[i].info,
                                Price = Convert.ToDouble(product.data[i].price),
                                Discount = product.data[i].discount,
                                Category_Name = product.data[i].category_name,
                                Category_ID = product.data[i].pcid,
                                Inventory = product.data[i].inventory,
                                Preview = PSAProc.GetUrl(product.data[i].avatar),
                                isUpdate = update,
                                isDelete = delete,
                                isDeleteCol = update == true ? 1 : 0
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2PDT-L1PVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2PDT-L1PVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L1PVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2PDT-L1PVM-" + err.ToString());
                }
            }
        }
    }
}