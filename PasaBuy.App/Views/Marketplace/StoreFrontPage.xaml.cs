using PasaBuy.App.DataService;
using PasaBuy.App.Views.Master;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreFrontPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogListPage" /> class.
        /// </summary>
        public StoreFrontPage()
        {
            InitializeComponent();
            this.BindingContext = CatalogDataService.Instance.CatalogPageViewModel;
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            //((MainPage)App.Current.MainPage).Detail = new NavigationPage(new StoreBrowserPage());
        }

        /// <summary>
        /// Invoked when option button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void OptionButton_Clicked(object sender, EventArgs e)
        {
            //Do something...
        }
    }
}