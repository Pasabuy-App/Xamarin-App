﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentsView : ContentPage
    {
        public PaymentsView()
        {
            InitializeComponent();
            this.BindingContext = new PasaBuy.App.ViewModels.MobilePOS.PaymentsViewModel();
        }
    }
}