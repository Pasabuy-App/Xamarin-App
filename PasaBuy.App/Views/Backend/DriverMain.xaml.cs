
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Backend
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverMain : MasterDetailPage
    {
        public DriverMain()
        {
            InitializeComponent();
        }

        public void HideSidebar()
        {
            IsPresented = false;

        }







    }
}