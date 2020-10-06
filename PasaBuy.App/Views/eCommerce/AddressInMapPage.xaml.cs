using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressInMapPage : ContentPage
    {
        public static string street;
        public static string address_id;
        public static double lat;
        public static double lon;
        public AddressInMapPage()
        {
            InitializeComponent();
            StreetEntry.Text = street + " lan: " + lat.ToString() + " lon: " + lon.ToString() + " id: " + address_id;
        }
    }
}