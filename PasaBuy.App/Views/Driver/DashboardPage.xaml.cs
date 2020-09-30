using PasaBuy.App.Models.Driver;
using PasaBuy.App.ViewModels.Driver;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Position = Xamarin.Forms.Maps.Position;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            Title = "Dashboard";
        }

        private void AcceptOrderButton_Clicked(object sender, EventArgs e)
        {
          /*  Position pos1 = new Xamarin.Forms.Maps.Position(14.3313, 121.0505);
            Position pos2 = new Xamarin.Forms.Maps.Position(14.3369, 121.0557);
            
            var add1 = "San Pedro Metro Manila Philippines";
            var add2 = "Southwoods Ave Biñan Laguna Philippines";


            // NavigationViewModel.FetchPin(pos1, pos2, add1, add2, "Store", "Client", );
            Views.Driver.Navigation.StoreValue = add2;
            Views.Driver.Navigation.UserValue = add1;
            Views.Driver.Navigation.StorePosition = pos1;
            Views.Driver.Navigation.UserPosition = pos2;
            Navigation.PushModalAsync(new Navigation() );
            //Views.Driver.Navigation.FetchPin(pos1, pos2, add1, add2, "user", "store");*/

        }
    }
}