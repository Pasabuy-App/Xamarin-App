using PasaBuy.App.Controllers;
using PasaBuy.App.Views.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputEntry : ContentView
    {
        public InputEntry()
        {
            InitializeComponent();
        }

        public void PostStatus(object sender, EventArgs args)
        {

            Navigation.PushModalAsync(new PostStatusPage());
        }

        public void PostRequest(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new PostRequestPage());
        }
        public void PostSell(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new PostSellPage());
        }


    }
}