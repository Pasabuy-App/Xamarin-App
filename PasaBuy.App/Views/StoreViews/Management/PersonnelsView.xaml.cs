using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonnelsView : ContentPage
    {
        public PersonnelsView()
        {
            InitializeComponent();
            this.BindingContext = new PersonnelsViewModel();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void AddTapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddPersonnel());
            
        }

        private async void Update_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditPersonnel());
        }
    }
}