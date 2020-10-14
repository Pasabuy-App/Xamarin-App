using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PointOfSales : ContentPage
    {
        public PointOfSales()
        {
            InitializeComponent();
            this.BindingContext = new POSViewModel();
        }
    }
}