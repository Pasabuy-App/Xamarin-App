using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupRateDriver : PopupPage
    {
        public static string order_id = string.Empty;
        public static string avatar = string.Empty;
        public static string mover_name = string.Empty;
        public static string mover_id = string.Empty;

        public PopupRateDriver()
        {
            InitializeComponent();
            OrderID.Text = order_id;
            Avatar.Source = avatar;
            MoverName.Text = mover_name;
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}