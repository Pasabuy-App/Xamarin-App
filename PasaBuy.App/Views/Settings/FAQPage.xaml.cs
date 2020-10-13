using PasaBuy.App.DataService;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    /// <summary>
    /// Page to show the FAQ page details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FAQPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FAQPage" /> class.
        /// </summary>
        public FAQPage()
        {
            InitializeComponent();
            this.BindingContext = FAQDataService.Instance.FAQViewModel;
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}