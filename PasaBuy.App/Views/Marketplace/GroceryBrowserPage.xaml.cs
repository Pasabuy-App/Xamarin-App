﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.DataService;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.StoreDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryBrowserPage : ContentView
    {
        public static int LastIndex = 11;
        public GroceryBrowserPage()
        {
            InitializeComponent();
            //this.BindingContext = StoreDataService.Instance.RestaurantViewModel;
        }

        private void GroceriesTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as Groceries;
            StoreDetailsViewModel.store_id = item.Id;
            StoreDetailsViewModel.loadcategory(item.Id);
            StoreDetailsViewModel.loadstoredetails(item.Id);
            App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
        }

        private void RestaurantList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Groceries;
            if (GroceryBrowserViewModel.grocerystorelist.Last() == item && GroceryBrowserViewModel.grocerystorelist.Count() != 1)
            {
                if (GroceryBrowserViewModel.grocerystorelist.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    GroceryBrowserViewModel.LoadGrocery(item.Id);
                }
            }
        }

    }
}