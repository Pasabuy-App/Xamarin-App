﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingOrdersView : ContentView
    {
        public PendingOrdersView()
        {
            InitializeComponent();
        }

        private void PendingOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new TransactionDetailsView());
        }
    }
}