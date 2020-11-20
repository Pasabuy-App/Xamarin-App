using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Driver;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentView
    {
        public bool isBtn = false;
        public static int LastIndex;

        public static bool isFirstLoad = false;
        public HomePage()
        {
            InitializeComponent();
            listView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
            {
                ItemSpacing = 15
            };
            //VisualContainer visualContainer = listView.GetType().GetRuntimeProperties().First(p => p.Name == "VisualContainer").GetValue(listView) as VisualContainer;
            //scrollRows = visualContainer.GetType().GetRuntimeProperties().First(p => p.Name == "ScrollRows").GetValue(visualContainer) as ScrollAxisBase;
            //scrollRows.Changed += ScrollRows_Changed;

            LastIndex = 12;
            listView.Scrolled += OnCollectionViewScrolled;

        }
        async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            /*Debug.WriteLine("HorizontalDelta: " + e.HorizontalDelta);
            Debug.WriteLine("VerticalDelta: " + e.VerticalDelta);
            Debug.WriteLine("HorizontalOffset: " + e.HorizontalOffset);
            Debug.WriteLine("VerticalOffset: " + e.VerticalOffset);
            Debug.WriteLine("FirstVisibleItemIndex: " + e.FirstVisibleItemIndex);
            Debug.WriteLine("CenterItemIndex: " + e.CenterItemIndex);*/
            if (e.LastVisibleItemIndex > LastIndex)
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    HomepageViewModel.LoadData(LastIndex.ToString());
                    LastIndex += 7;
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
            }
        }
        private void ScrollRows_Changed(object sender, ScrollChangedEventArgs e) 
        {
           

            //To include header if used
            //var header = (listView.HeaderTemplate != null && !listView.IsStickyHeader) ? 1 : 0;

            ////To include footer if used
            //var footer = (listView.FooterTemplate != null && !listView.IsStickyFooter) ? 1 : 0;
            //var totalItems = listView.DataSource.DisplayItems.Count + header + footer;

            //if ((lastIndex == totalItems - 1))
            //{
            //    if (!isAlertShown)
            //    {
            //        if (isFirstLoad)
            //        {
            //            //new Alert("Alert", "End of list reached...", "Ok");
            //            HomepageViewModel.LoadData(LastIndex.ToString());
            //            LastIndex += 7;
            //        }
            //        else
            //        {
            //            isFirstLoad = true;
            //        }
            //        isAlertShown = true;
            //    }
            //}
            //else
            //    isAlertShown = false;
        }


    }
}