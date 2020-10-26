using PasaBuy.App.ViewModels.Driver;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverDocuments : ContentPage
    {
        public DriverDocuments()
        {
            InitializeComponent();
            this.BindingContext = new DriverDocumentsViewModel();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}