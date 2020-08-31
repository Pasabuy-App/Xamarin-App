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
    public partial class PostStatusPage : ContentPage
    {
        int clickTotal = 0;


        public PostStatusPage()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void OnImageButtonClicked(object sender, EventArgs e)
        {
            clickTotal += 1;
            testlabel.Text = $"{clickTotal} ImageButton click{(clickTotal == 1 ? "" : "s")}";
        }
    }
}