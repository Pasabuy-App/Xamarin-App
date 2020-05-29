using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Profile
{
    /// <summary>
    /// Page to show article master page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterPage" /> class.
        /// </summary>
        public MasterPage()
        {
            InitializeComponent();

            Application.Current.Resources.TryGetValue("PrimaryColor", out var primeColor);

            var iconview = HomeGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 0);
            var iconTar = iconview as Label;
            if (iconTar != null)
            {
                iconTar.TextColor = (Color)primeColor;
                viewModel.iconLabel = iconTar;
            }

            var textView = HomeGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 1);
            var textTar = textView as Label;
            if (textTar != null)
            {
                textTar.TextColor = (Color)primeColor;
                viewModel.textLabel = textTar;
            }
        }
    }
}