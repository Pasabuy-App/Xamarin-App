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
          
        }

        public async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
           
        }

        private async void Add2CartClicked(object sender, EventArgs e)
        {
            //new Controllers.Notice.Alert("OK", "Ok", "Ok");
         
        }
    }
}