using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverChatMessagePage : ContentPage
    {
        public DriverChatMessagePage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            if (DriverChatMessageViewModel.myPage != "home")
            {
                MessagesViewModel.storeChatList.Clear();
                MessagesViewModel.LoadMesssage("");
            }
            DriverChatMessageViewModel.refresh = 1;
            return base.OnBackButtonPressed();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            if (DriverChatMessageViewModel.myPage != "home")
            {
                MessagesViewModel.storeChatList.Clear();
                MessagesViewModel.LoadMesssage("");
            }
            DriverChatMessageViewModel.refresh = 1;
            Navigation.PopModalAsync();
        }
    }
}