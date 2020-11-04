using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using Plugin.Media;
using System;
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
        public static string pcid = "0";
        public static string name = string.Empty;
        public static string info = string.Empty;
        public static string price = string.Empty;
        public static string discount = string.Empty;
        public static string avatar = string.Empty;
        public static string category_name = string.Empty;
        public static string inventory = string.Empty;
        public static string inventories = string.Empty;
        public AddProductView()
        {
            InitializeComponent();
            this.BindingContext = new AddProductViewModel();
            if (pdid != "0")
            {
                Title = "Edit Product";
                ProductNames.Text = name;
                Shorts.Text = info;
                Prices.Text = price;
                ProductCategory.Text = category_name;
                Discount.Text = discount;
                productImage.Source = PSAProc.GetUrl(avatar);
                catid = pcid;
                inventories = inventory;
                Inventory.Text = inventory == "true" ? "Yes" : "No";
            }
            else
            {
                Title = "Add Product";
                productImage.Source = "https://pasabuy.app/wp-content/uploads/2020/10/management-product.png";
                ProductNames.Text = "";
                Shorts.Text = "";
                Prices.Text = "";
                Discount.Text = "";
                ProductCategory.Text = "";
                Inventory.Text = "";
                inventories = string.Empty;
                catid = string.Empty;
            }
        }

        void RemoveProductImageCommand(object sender, EventArgs args)
        {
            productImage.Source = "https://pasabuy.app/wp-content/uploads/2020/10/management-product.png";
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

        private async void Discard(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private void AddProduct(object sender, EventArgs e)
        {
            try
            {
                ProductName.HasError = ProductNames.Text == null || string.IsNullOrWhiteSpace(ProductNames.Text) ? true : false;
                Short.HasError = Shorts.Text == null || string.IsNullOrWhiteSpace(Shorts.Text) ? true : false;
                Price.HasError = Prices.Text == null || string.IsNullOrWhiteSpace(Prices.Text) ? true : false;
                Category.HasError = ProductCategory.Text == null || string.IsNullOrWhiteSpace(ProductCategory.Text) ? true : false;
                Disc.HasError = Discount.Text == null || string.IsNullOrWhiteSpace(Discount.Text) ? true : false;
                Inventories.HasError = Inventory.Text == null || string.IsNullOrWhiteSpace(Inventory.Text) ? true : false;
                string filepath = filePath == null || filePath == "" || filePath == string.Empty ? "" : filePath;

                if (ProductName.HasError == false && Short.HasError == false && Price.HasError == false && Category.HasError == false && Disc.HasError == false && Inventories.HasError == false)
                {
                    if (!IsRunning.IsRunning)
                    {
                        IsRunning.IsRunning = true;
                        if (pdid == "0")
                        {
                            Http.TindaPress.Product.Instance.Insert(filepath, catid, ProductNames.Text, Shorts.Text, Prices.Text, Discount.Text, inventories, async (bool success, string data) =>
                            {
                                if (success)
                                {
                                    ProductsView.LastIndex = 11;
                                    ProductsView.isFirstLoad = false;
                                    ProductsView.Offset = 0;
                                    ProductViewModel.RefreshProduct("");
                                    await Navigation.PopModalAsync();
                                    IsRunning.IsRunning = false;
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    IsRunning.IsRunning = false;
                                }
                            });
                        }
                        else
                        {
                            Http.TindaPress.Product.Instance.Update(filepath, pdid, catid, ProductNames.Text, Shorts.Text, Prices.Text, Discount.Text, inventories, async (bool success, string data) =>
                            {
                                if (success)
                                {
                                    ProductsView.LastIndex = 11;
                                    ProductsView.isFirstLoad = false;
                                    ProductsView.Offset = 0;
                                    pdid = "0";
                                    ProductViewModel.RefreshProduct("");
                                    await Navigation.PopModalAsync();
                                    IsRunning.IsRunning = false;
                                }
                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                    IsRunning.IsRunning = false;
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-IU1APV.", "OK");
                IsRunning.IsRunning = false;
            }
        }

        private void ProductCategory_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            catid = ProductCategory.SelectedValue.ToString();
        }

        private void Inventory_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            inventories = Inventory.SelectedItem.ToString() == "Yes" ? "true" : "false";
        }
    }
}