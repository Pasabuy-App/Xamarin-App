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
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
            if (MasterView.MyType == "store")
            {
                Title = "POS Terminal";
            }
            if (MasterView.MyType == "mover")
            {
                Title = "Dashboard";
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                //Context.ScannedText = e.NewTextValue;
                //await Context.SearchProductWhenScan();
            }
        }
    }
}