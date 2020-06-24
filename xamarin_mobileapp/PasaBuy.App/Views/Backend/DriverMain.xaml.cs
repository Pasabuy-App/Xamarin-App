using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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