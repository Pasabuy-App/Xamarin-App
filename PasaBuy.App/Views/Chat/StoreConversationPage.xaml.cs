using FFImageLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Chat
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreConversationPage : ContentPage
    {
        public StoreConversationPage()
        {
            InitializeComponent();
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}