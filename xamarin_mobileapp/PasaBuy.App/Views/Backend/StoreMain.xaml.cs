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
    public partial class StoreMain : MasterDetailPage
    {
        public StoreMain()
        {
            InitializeComponent();
        }

        public void HideSidebar()
        {
            IsPresented = false;
        }
    }
}