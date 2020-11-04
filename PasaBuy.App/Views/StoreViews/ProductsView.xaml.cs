using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsView : ContentPage
    {
        public static int LastIndex = 11;
        public static int Offset = 0;
        public static bool isFirstLoad = false;
        public int count = 0;
        public ICommand AddProductCommand { get; set; }

        public ProductsView()
        {
            InitializeComponent();
            this.BindingContext = new ProductViewModel();
            SearchText.SearchButtonPressed += SearchButtonPress;
        }

        void SearchButtonPress(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (!string.IsNullOrWhiteSpace(searchBar.Text))
            {
                ProductViewModel.RefreshProduct(searchBar.Text);
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}