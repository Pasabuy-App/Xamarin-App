using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.DataService;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.StoreDetail;
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
    public partial class GroceryBrowserPage : ContentView
    {
        public static int LastIndex = 11;
        public GroceryBrowserPage()
        {
            InitializeComponent();
            //this.BindingContext = StoreDataService.Instance.RestaurantViewModel;
        }


    

   


     
    }
}