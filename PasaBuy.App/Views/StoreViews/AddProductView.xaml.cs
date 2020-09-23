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
    public partial class AddProductView : ContentPage
    {
        public AddProductView()
        {
            this.BindingContext = new AddProductViewModel();
            InitializeComponent();
           
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Scan_Clicked(object sender, EventArgs e)
        {
            //ScanAsync();
        }

        async void OnSupplier_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSupplierAsync();
        }
        async void OnUnit_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenSourceUnitAsync();
        }

        async void OnCategory_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            //await Context.OpenCategoryAsync();
        }

        private AddProductViewModel Context => (AddProductViewModel)this.BindingContext;

        void Handle_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            Context.ItemIndex = -1;
        }

        void Handle_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            Context.ItemIndex = e.ItemIndex;
        }
    }
}