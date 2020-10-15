using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.ViewModels.Settings;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    /// <summary>
    /// Page to show my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPage
    {
        private bool isEnable = false;
        private string add_id = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAddressPage" /> class.
        /// </summary>
        public AddressPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();

        }

        public void AddButtonClicked(object sender, EventArgs e)
        {
            if (!isEnable)
            {
                isEnable = true;
                AddAddressPage.addPath = "New";
                Navigation.PushModalAsync(new AddAddressPage());
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(1000);
                    isEnable = false;
                });
            }
        }

        private async void myAddress_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {
                var item = e.ItemData as Address;
                if (item.SelectedAddress != "0")
                {
                    bool answer = await DisplayAlert("Delete Address?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        DataVice.Address.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, item.SelectedAddress, (bool success, string data) =>
                        {
                            if (success)
                            {
                                AddressViewModel.addressDetails.Clear();
                                AddressViewModel.LoadData();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }

        }
    }

}