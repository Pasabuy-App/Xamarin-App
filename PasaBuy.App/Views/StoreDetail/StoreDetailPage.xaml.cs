using System;
using PasaBuy.App.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreDetail
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreDetailPage : ContentPage
    {
        public StoreDetailPage()
        {
            InitializeComponent();
            this.BindingContext = MoviesDataService.Instance.MoviesPageViewModel;
        }
    }
}