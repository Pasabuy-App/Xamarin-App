
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulesView : ContentPage
    {
        public SchedulesView()
        {
            InitializeComponent();
            this.BindingContext = new PasaBuy.App.ViewModels.MobilePOS.ScheduleViewModel();
        }
    }
}