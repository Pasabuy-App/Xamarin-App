﻿using PasaBuy.App.Models.Currency;
using PasaBuy.App.ViewModels.Currency;
using System;
using System.Linq;
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
    public partial class WalletSaving : ContentView
    {
        public static int LastIndex = 11;
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletSaving"/> class.
        /// </summary>
        public WalletSaving()
        {
            InitializeComponent();
            LastIndex = 11;
        }

        private void SfListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as WalletModel;
            if (WalletSavingViewModel._SavingsList.Last() == item && WalletSavingViewModel._SavingsList.Count() != 1)
            {
                if (WalletSavingViewModel._SavingsList.IndexOf(item) >= LastIndex)
                {
                    WalletSavingViewModel.LoadData((LastIndex += 1).ToString());
                    LastIndex += 6;
                }
            }
        }
    }
}