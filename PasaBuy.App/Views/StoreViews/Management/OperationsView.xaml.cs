﻿using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationsView : ContentPage
    {
        public OperationsView()
        {
            InitializeComponent();
            this.BindingContext = new OperationsViewModel();

        }
    }
}