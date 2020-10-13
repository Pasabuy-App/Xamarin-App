
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demoguy : ContentPage
    {
        public Demoguy()
        {
            InitializeComponent();
        }

        void Handle_SlideCompleted(object sender, System.EventArgs e)
        {
        }
    }
}