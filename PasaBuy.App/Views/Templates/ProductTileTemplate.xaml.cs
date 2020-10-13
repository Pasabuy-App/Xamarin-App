using PasaBuy.App.Views.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Templates
{
    /// <summary>
    /// Product tile template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductTileTemplate : Grid
    {
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object),
         typeof(ProductTileTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTileTemplate"/> class.
        /// </summary>
		public ProductTileTemplate()
        {
            InitializeComponent();
        }

        private void ProductTapped(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new DetailPage());
            //new Alert("Something went Wrong", "Please contact administrator." + ' ' , "OK");

        }
    }
}