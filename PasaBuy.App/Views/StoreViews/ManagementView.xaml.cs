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
    public partial class ManagementView : ContentPage
    {
        public int count = 0;
        public ManagementView()
        {
            InitializeComponent();
        }

        async void Management_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                if (e.ItemData != null)
                {
                    PasaBuy.App.Models.MobilePOS.Management pageData = (e.ItemData as PasaBuy.App.Models.MobilePOS.Management);
                    Page page = (Page)Activator.CreateInstance(pageData.Type);
                    await Navigation.PushAsync(page);
                    await Task.Delay(100);
                    count = 0;
                }
            }
        }
    }
}