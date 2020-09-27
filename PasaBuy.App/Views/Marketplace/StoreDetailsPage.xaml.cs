using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.eCommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreDetailsPage : ContentPage
    {
        public StoreDetailsPage()
        {
            InitializeComponent();
           

        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void GoToCart(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CartPage());
        }


        

        /*private void CategoryTapped(object sender, EventArgs e)
        {*/
        //var item = e.ItemData as StoreDetails;
        /* var btn = (TapGestureRecognizer)sender;
         var id = btn.ClassId;
         new Alert("sample", "data: "+ id, "ok");*/

        //}
    }
}