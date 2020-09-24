using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
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
    public partial class CategoryView : ContentPage
    {
        public int count = 0;
        public CategoryView()
        {
            InitializeComponent();
            AddCategoryButton.Clicked += AddCategoryClicked;
        }

        private async void AddCategoryClicked(object sender, EventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                AddCategoryButton.IsEnabled = false;
                await Task.Delay(200);
                await PopupNavigation.Instance.PushAsync(new PopupAddCategory());
                AddCategoryButton.IsEnabled = true;
                count = 0;
            }
        }

    }
}