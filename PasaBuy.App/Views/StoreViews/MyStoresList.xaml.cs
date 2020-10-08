using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyStoresList : ContentPage
    {
        public MyStoresList()
        {
            InitializeComponent();
            this.BindingContext = new MyStoreListViewModel();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            MasterView.MyType = "store";
            App.Current.MainPage = new NavigationView();
        }
    }
}