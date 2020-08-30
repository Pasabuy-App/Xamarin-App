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
            new Alert("Demoguy Notice", "Adding address entry is not yet implemented. Thank you for your patience!", "AGREE");
        }
    }
}