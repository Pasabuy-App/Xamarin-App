using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverMenu : ContentPage
    {
        public DriverMenu()
        {
            InitializeComponent();

            Application.Current.Resources.TryGetValue("PrimaryColor", out var primeColor);

            var iconview = DashboardGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 0);
            var iconTar = iconview as Label;
            if (iconTar != null)
            {
                iconTar.TextColor = (Color)primeColor;
                viewModel.iconLabel = iconTar;
            }

            var textView = DashboardGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 1);
            var textTar = textView as Label;
            if (textTar != null)
            {
                textTar.TextColor = (Color)primeColor;
                viewModel.textLabel = textTar;
            }
        }



    }
}