using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportsView : ContentPage
    {
        public ReportsView()
        {
            InitializeComponent();
        }

        public ReportsViewModel Context => (ReportsViewModel)this.BindingContext;

        async void Handle_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem.ToString() == TextsTranslateManager.Translate("BySale"))
            //{
            //    await Context.LoadTop10ProductByRevenueAsync();
            //}
            //else
            //{
            //    await Context.LoadTop10ProductByQuantityAsync();
            //}
        }
    }
}