using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostSellPage : ContentPage
    {
        public PostSellPage()
        {
            InitializeComponent();
       
            itemCategory.ItemsSource = new List<string>
            {
                "Food & Drinks",
                "Apparels",
                "Gadgets"
            };

            vehicleType.ItemsSource = new List<string>
            {
                "Bicycle",
                "Motorcycle",
                "Four-Wheeled Vehicle"
            };
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        
    }
}