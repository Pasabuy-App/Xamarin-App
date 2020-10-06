﻿using PasaBuy.App.ViewModels.Currency;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupSendWalletCredit : PopupPage
    {
        public PopupSendWalletCredit()
        {
            InitializeComponent();
            this.BindingContext = new WalletSavingViewModel();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}