﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    /// <summary>
    /// My wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyWalletPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyWalletPage"/> class.
        /// </summary>
        public MyWalletPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}