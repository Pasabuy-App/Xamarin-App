using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsAddressView : ContentPage
    {
        public int count = 0;
        public SettingsAddressView()
        {
            InitializeComponent();
            AddAddressButton.IsVisible = false;
            AddAddressButton.Clicked += AddAddressClicked;
            SettingsAddressViewModel.addressList.CollectionChanged += CollectionChanges; 
        }
        private async void CollectionChanges(object sender, EventArgs e)
        {
            Console.WriteLine("Count: " + SettingsAddressViewModel.addressList.Count() );
            if (SettingsAddressViewModel.addressList.Count() > 0)
            {
                AddAddressButton.IsVisible = false;
            }
            else
            {
                AddAddressButton.IsVisible = true;
            }
        }
        private async void AddAddressClicked(object sender, EventArgs e)
        {
            AddAddressView.adid = "0";
            await Navigation.PushAsync(new AddAddressView());
            //new Alert("Ok", "Ok", "Ok");
        }

        /*private async void Update_Tapped(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                var btn = sender as Grid;
                AddAddressView.adid = btn.ClassId;
                await Navigation.PushAsync(new AddAddressView());
                await Task.Delay(200);
                count = 0;
            }
        }*/

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                bool answer = await DisplayAlert("Delete Address?", "Are you sure to delete this?", "Yes", "No");
                if (answer)
                {
                    try
                    {
                        var btn = sender as Grid;
                        TindaPress.Address.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                SettingsAddressViewModel.addressList.Clear();
                                SettingsAddressViewModel.LoadData();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                    catch (Exception)
                    {
                        new Alert("Something went Wrong", "Please contact administrator. Error Code: 20416.", "OK");
                    }
                }
                await Task.Delay(200);
                count = 0;
            }
        }
    }
}