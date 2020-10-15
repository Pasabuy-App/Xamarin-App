using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        public static string productid;
        public static string productname;
        public static string shortinfo;
        public static string productimage;
        public static string price;
        public static string totalprice;
        public bool isCartClicked = false;
        public int qty = 1;
        public ProductDetail()
        {
            InitializeComponent();
            this.BindingContext = new ProductDetailViewModel();
            ProductLogo.Source = productimage;
            ProductName.Text = productname;
            Short_Info.Text = shortinfo;
            Price.Text = "₱" + price;
            isCartClicked = false;

            bool itemExists = ViewModels.eCommerce.CartPageViewModel.cartDetails.Any(item =>
            {
                return (item.ID == productid);
            });
            if (!itemExists)
            {
                numericUpDown.Value = 1;
                TotalPrice.Text = "Total: ₱" + totalprice;
            }
            else
            {
                foreach (Models.Marketplace.ProductList item in ViewModels.eCommerce.CartPageViewModel.cartDetails)
                {
                    if (item.ID == productid)
                    {
                        numericUpDown.Value = item.TotalQuantity;
                        Price.Text = "₱" + item.ActualPrice;
                        double totalprice = item.TotalQuantity * item.ActualPrice;
                        TotalPrice.Text = "Total: ₱" + totalprice.ToString();
                    }
                }
            }
        }

        public async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            double total = Convert.ToDouble(price) * Convert.ToDouble(e.Value);
            TotalPrice.Text = "Total: ₱" + Convert.ToString(total);
            totalprice = Convert.ToString(total);
            qty = Convert.ToInt32(e.Value);
            //Console.WriteLine("Value: " + e.Value + " total: " + total);
        }

        private async void Add2CartClicked(object sender, EventArgs e)
        {
            //new Controllers.Notice.Alert("OK", "Ok", "Ok");
            if (!isCartClicked)
            {
                isCartClicked = true;
                ViewModels.eCommerce.CartPageViewModel.InsertCart(StoreDetailsViewModel.store_id, productid, productname, shortinfo, productimage, Convert.ToDouble(price), Convert.ToDouble(totalprice), qty);

                await Navigation.PopModalAsync();
                await Task.Delay(500);
                isCartClicked = false;
            }
        }
    }
}