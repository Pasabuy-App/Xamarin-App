using PasaBuy.App.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    /// <summary>
    /// Page to show the my order list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrdersPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyOrdersPage" /> class.
        /// </summary>
        public MyOrdersPage()
        {
            InitializeComponent();
            this.BindingContext = MyOrdersDataService.Instance.MyOrdersPageViewModel;
        }
    }
}