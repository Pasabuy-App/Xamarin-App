using PasaBuy.App.DataService;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    /// <summary>
    /// Page to show the Restaurant page details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreBrowserPage : ContentView
    {
        public static bool isTapped = false;
        public static int LastIndex = 11;
        public StoreBrowserPage()
        {
            InitializeComponent();
            isTapped = false;
            //this.BindingContext = StoreDataService.Instance.RestaurantViewModel;
        }
        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

           
        }

        /// <summary>
        /// Invoked when search button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Invoked when back to title button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Invokes when search box Animation completed.
        /// </summary>
        private void SearchBoxAnimationCompleted()
        {
          
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        private void SearchExpandAnimationCompleted()
        {
        }

        private async void StoreTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Categories;
                //App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                //StoreDetailsViewModel.loadcategory(item.Id);
                //StoreDetailsViewModel.loadstoredetails(item.Id);
                //StoreListPage.myTitle = item.Title;
                //Console.WriteLine("Title " + item.Title);
                StoreListPage.catid = item.Id;
                StoreBrowserViewModel.LoadStore(item.Id, "");
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreListPage());
                await System.Threading.Tasks.Task.Delay(200);
                isTapped = false;
            }
        }

        private void RestaurantList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
           /* var item = e.ItemData as Store;
            if (StoreBrowserViewModel.storelist.Last() == item && StoreBrowserViewModel.storelist.Count() != 1)
            {
                if (StoreBrowserViewModel.storelist.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    StoreBrowserViewModel.LoadStore(item.Id);
                }
            }*/
        }
    }
}