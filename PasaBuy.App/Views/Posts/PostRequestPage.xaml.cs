using PasaBuy.App.CustomRenderers;
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
    public partial class PostRequestPage : ContentPage
    {

        public PostRequestPage()
        {
            InitializeComponent();
            
            vehicleType.ItemsSource = new List<string>
            {
                "Bicycle",
                "Motorcycle",
                "Four-Wheeled Vehicle"
            };

            postStyle.ItemsSource = new List<string>
            {
                "Option 1",
                "Option 2"
            
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