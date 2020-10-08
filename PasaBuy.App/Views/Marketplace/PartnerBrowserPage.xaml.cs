using PasaBuy.App.ViewModels.Marketplace;
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
    public partial class PartnerBrowserPage : ContentView
    {
        public PartnerBrowserPage()
        {
            InitializeComponent();
            this.BindingContext = new PartnerBrowserViewModel();
            PartnerBrowserViewModel.LoadCategory();
        }
    }
}