using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Advisory
{
    /// <summary>
    /// Page to list out article items.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleList" /> class.
        /// </summary>
        public ArticleList()
        {
            InitializeComponent();
        }
    }
}