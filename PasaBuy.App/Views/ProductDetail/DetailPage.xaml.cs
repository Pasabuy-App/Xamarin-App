using PasaBuy.App.ViewModels.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.ProductDetail
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPage" /> class.
        /// </summary>
        public DetailPage()
        {
            InitializeComponent();
            this.BindingContext = new DetailPageViewModel();
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                Rotator.ItemTemplate = (DataTemplate)this.Resources["LandscapeTemplate"];
            }
            else
            {
                Rotator.ItemTemplate = (DataTemplate)this.Resources["PortraitTemplate"];
            }
        }

        private void backButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void CartClick(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new PasaBuy.App.Views.eCommerce.CartPage());
        }
    }
}