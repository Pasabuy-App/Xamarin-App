using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductView : ContentPage
    {
        public static string filePath = string.Empty;
        public static string catid = string.Empty;
        public static string pdid = "0";
        public AddProductView()
        {
            //this.BindingContext = new AddProductViewModel();
            InitializeComponent();
            if (pdid != "0")
            {
                try
                {
                    TindaPress.Product.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", pdid, "1", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            ProductData product = JsonConvert.DeserializeObject<ProductData>(data);
                            for (int i = 0; i < product.data.Length; i++)
                            {
                                ProductNames.Text = product.data[i].product_name;
                                Shorts.Text = product.data[i].short_info;
                                Prices.Text = product.data[i].price;
                                ProductCategory.Text = product.data[i].cat_name;
                                Longs.Text = product.data[i].long_info;
                                SKUs.Text = product.data[i].sku;
                                Weights.Text = product.data[i].weight;
                                Dimensions.Text = product.data[i].dimension;
                                productImage.Source = PSAProc.GetUrl(product.data[i].preview);
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
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20430.", "OK");
                }
            }
            else
            {
                productImage.Source = "Idcard.png";
                ProductNames.Text = "";
                Shorts.Text = "";
                Prices.Text = "";
                ProductCategory.Text = "";
                Longs.Text = "";
                SKUs.Text = "";
                Weights.Text = "";
                Dimensions.Text = "";
            }
        }

        void RemoveProductImageCommand(object sender, EventArgs args)
        {
            productImage.Source = "Idcard.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                CompressionQuality = 30
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            productImage.Source = imageSource;
            filePath = file.Path;
        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                SaveMetaData = false,
                CompressionQuality = 30,
                MaxWidthHeight = 1024
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            productImage.Source = imageSource;
            filePath = file.Path;

        }

        public async void BackButtonClicked(object sender, EventArgs e)
        {
            //ProductViewModel.RefreshData();
            await Navigation.PopModalAsync();
        }

        private void Scan_Clicked(object sender, EventArgs e)
        {
            //ScanAsync();
        }

        void OnSupplier_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSupplierAsync();
        }
        void OnUnit_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSourceUnitAsync();
        }

        void OnCategory_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenCategoryAsync();
        }

        //private AddProductViewModel Context => (AddProductViewModel)this.BindingContext;

        void Handle_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            //Context.ItemIndex = -1;
        }

        void Handle_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            //Context.ItemIndex = e.ItemIndex;
        }

        private void Discard(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void AddProduct(object sender, EventArgs e)
        {
            ProductName.HasError = ProductNames.Text == null || string.IsNullOrWhiteSpace(ProductNames.Text) ? true : false;
            Short.HasError = Shorts.Text == null || string.IsNullOrWhiteSpace(Shorts.Text) ? true : false;
            Price.HasError = Prices.Text == null || string.IsNullOrWhiteSpace(Prices.Text) ? true : false;
            Category.HasError = ProductCategory.Text == null || string.IsNullOrWhiteSpace(ProductCategory.Text) ? true : false;
            string lon = Longs.Text == null || string.IsNullOrWhiteSpace(Longs.Text) ? "N/A" : Longs.Text;
            string sku = SKUs.Text == null || string.IsNullOrWhiteSpace(SKUs.Text) ? "N/A" : SKUs.Text;
            string weight = Weights.Text == null || string.IsNullOrWhiteSpace(Weights.Text) ? "N/A" : Weights.Text;
            string dimension = Dimensions.Text == null || string.IsNullOrWhiteSpace(Dimensions.Text) ? "N/A" : Dimensions.Text;
            string filepath = filePath == null || filePath == "" || filePath == string.Empty ? "" : filePath;

            if (ProductName.HasError == false && Short.HasError == false && Price.HasError == false && Category.HasError == false)
            {
                try
                {
                    if (pdid == "0")
                    {
                        TindaPress.Product.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, PSACache.Instance.UserInfo.stid, ProductNames.Text, filepath, Shorts.Text, lon, sku, Prices.Text, weight, dimension, (bool success, string data) =>
                        {
                            if (success)
                            {
                                ProductsView.LastIndex = 11;
                                ProductsView.isFirstLoad = false;
                                ProductsView.Offset = 0;
                                ProductViewModel.productsList.Clear();
                                ProductViewModel.LoadData("");
                                Navigation.PopAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                            }
                        });
                    }
                    else
                    {
                        TindaPress.Product.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, pdid, PSACache.Instance.UserInfo.stid, ProductNames.Text, filepath, Shorts.Text, lon, sku, Prices.Text, weight, dimension, (bool success, string data) =>
                        {
                            if (success)
                            {
                                ProductsView.LastIndex = 11;
                                ProductsView.isFirstLoad = false;
                                ProductsView.Offset = 0;
                                pdid = "0";
                                ProductViewModel.productsList.Clear();
                                ProductViewModel.LoadData("");
                                Navigation.PopAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                            }
                        });
                    }
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20430.", "OK");
                }
            }
        }

        private void ProductCategory_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            catid = ProductCategory.SelectedValue.ToString();
        }
    }
}