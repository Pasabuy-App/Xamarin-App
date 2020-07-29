using PasaBuy.App.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Market
{
    /// <summary>
    /// Page to show Catalog tile.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogTilePage : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogTilePage" /> class.
        /// </summary>
        public CatalogTilePage()
        {
            InitializeComponent();
            this.BindingContext = CatalogDataService.Instance.CatalogPageViewModel;
        }
    }
}