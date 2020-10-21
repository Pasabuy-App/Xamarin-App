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
        public static int variants_id = 0;
        public static string variants_options;
        public ProductDetail()
        {
            InitializeComponent();
            this.BindingContext = new ProductDetailViewModel();
          
        }

        public async void BackButtonClicked(object sender, EventArgs e)
        {
            variants_id = 0;
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                variants_id = 0;
            });
            return base.OnBackButtonPressed();
        }

        private void numericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
           
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await CartGrid.FadeTo(0.5, 200);
            await CartGrid.FadeTo(1, 200);
        }

        private void Options_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                //variants_options = (sender as Syncfusion.XForms.Buttons.SfRadioButton).Text;
                variants_id = Convert.ToInt32((sender as Syncfusion.XForms.Buttons.SfRadioButton).ClassId);
                //new Controllers.Notice.Alert("Something went Wrong", "test " + variants_options + " id: " + variants_id, "OK");
            }
        }
    }
}