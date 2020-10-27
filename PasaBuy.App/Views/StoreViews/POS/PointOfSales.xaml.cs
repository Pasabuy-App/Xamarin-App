using PasaBuy.App.ViewModels.MobilePOS;
using System;

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

        private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            POSViewModel._quantity.Clear();
        }
    }
}