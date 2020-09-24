using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsView : ContentPage
    {

        public ICommand AddProductCommand { get; set; }

        public ProductsView()
        {
            InitializeComponent();
            AddProductButton.Clicked += AddProductClicked;
        }

        private async void AddProductClicked(object sender, EventArgs e)
        {
            //var testPage = new NavigationPage(new AddProductView());
            //Navigation.PushAsync(testPage);
            if (AddProductButton.IsEnabled == true)
            {
                AddProductButton.IsEnabled = false;
                AddProductCommand = new Command<Type>(async (Type pageType) =>
                {
                    Page page = (Page)Activator.CreateInstance(pageType);
                    await Navigation.PushAsync(page);
                });
                BindingContext = this;
                await Task.Delay(200);
                AddProductButton.IsEnabled = true;
            }
        }
    }
}