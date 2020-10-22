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
            Device.BeginInvokeOnMainThread(() =>
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
                //(sender as Syncfusion.XForms.Buttons.SfRadioButton).Text = "";
                //variants_id = Convert.ToInt32((sender as Syncfusion.XForms.Buttons.SfRadioButton).ClassId);
                //new Controllers.Notice.Alert("Something went Wrong", "test " + variants_id, "1 " + (sender as Syncfusion.XForms.Buttons.SfRadioButton).Text);
                ProductDetailViewModel.InsertVariants((sender as Syncfusion.XForms.Buttons.SfRadioButton).ClassId, (sender as Syncfusion.XForms.Buttons.SfRadioButton).Text);
            }
        }

        private void checkBox_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            // TODO : if check, insert to observable collection. if uncheck, check the option id then remove it from observable collection.
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ProductDetailViewModel.InsertAddOns( (sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId, (sender as Syncfusion.XForms.Buttons.SfCheckBox).Text);
            }
            else
            {
                ProductDetailViewModel.RemoveAddOns( (sender as Syncfusion.XForms.Buttons.SfCheckBox).ClassId, (sender as Syncfusion.XForms.Buttons.SfCheckBox).Text);
            }
        }

    }
}