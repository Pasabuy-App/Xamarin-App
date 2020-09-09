using PasaBuy.App.Views.Master;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Controllers.Notice;

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

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Address", "Are you sure you want to delete this?", "Yes", "No");
            if (answer == true)
            {
                try
                {
                    //new Alert("Yes", "Deleted successfully!", "OK");
                    /*DataVice.Address.Instance.Delete("1", "GNAyLSwWVKkeemhktBSqa9UjGlLXxzUEOdfoCojYJAD", "11", "", (bool success, string message) =>
                    {
                        Console.WriteLine(message);
                    });*/
                }
                catch (Exception ex)
                {
                    new Alert("Something went Wrong", "Please contact administrator.", "OK");
                    Console.WriteLine("Error: " + ex);
                }
            }
        }
    }
}