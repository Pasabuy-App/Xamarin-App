using PasaBuy.App.Views.Master;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.ViewModels.Settings;
using PasaBuy.App.Local;

namespace PasaBuy.App.Views.Settings
{
    /// <summary>
    /// Page to show my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPage
    {
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
            Navigation.PushModalAsync(new AddAddressPage());
        }

        private async void myAddress_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            string addressid = string.Empty;
            var item = e.ItemData as Address;
            addressid = item.SelectedAddress;
            bool answer = await DisplayAlert("Delete Address?", "Are you sure to delete this?", "Yes", "No");
            if (answer)
            {
                DataVice.Address.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, addressid, (bool success, string data) =>
                {

                });
            }
        }

    }
}