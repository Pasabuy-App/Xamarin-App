﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
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
    public partial class Dashboard : ContentPage
    {
        public bool isClicked = false;
        public Dashboard()
        {
            InitializeComponent();
            if (MasterView.MyType == "store")
            {
                this.BindingContext = new DashboardOrdersViewModel();
                Title = "Orders";
            }
            if (MasterView.MyType == "mover")
            {
                Title = "Dashboard";
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
   
            }
        }

        private async void NewOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isClicked)
            {
                isClicked = true;
                var item = e.ItemData as OrdersDataModel;
                TransactionDetailsView.id = item.ID;
                TransactionDetailsView.customer = item.Customer;
                TransactionDetailsView.orderid = item.OrderID;
                TransactionDetailsView.totalprice = item.TotalPrice;
                TransactionDetailsView.datecreated = item.Date_Time;
                TransactionDetailsView.method = item.Method;
                TransactionDetailsView.order_type = "Pending";
                TransactionDetailsView.stage_type = "pending";
                OrderDetailsViewModel.LoadOrder(item.Stage, item.ID);
                await Navigation.PushModalAsync(new TransactionDetailsView());
                await Task.Delay(500);
                isClicked = false;
            }
        }

        private  async void SfTabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (!isClicked)
            {
                isClicked = true;
                if (e.TabItem.Title == "New Orders")
                {
                    //new Alert("New Orders", "New Orders", "New Orders");
                    //DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("pending", "");
                }
                if (e.TabItem.Title == "Pending")
                {
                    //new Alert("Pending", "Pending", "Pending");
                    //DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("received", "");
                }
                if (e.TabItem.Title == "Declined")
                {
                    //DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("cancelled", "");
                    //new Alert("Declined", "Declined", "Declined");
                }
                if (e.TabItem.Title == "Completed")
                {
                    //DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("shipping", "");
                    //new Alert("Completed", "Completed", "Completed");
                }
                await Task.Delay(500);
                isClicked = false;
            }
        }

    }
}