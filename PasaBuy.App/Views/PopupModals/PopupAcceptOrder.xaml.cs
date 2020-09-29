﻿using PasaBuy.App.Controllers.Notice;
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
    public partial class PopupAcceptOrder : PopupPage
    {
        public PopupAcceptOrder()
        {
            InitializeComponent();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void AcceptOrder(object sender, EventArgs e)
        {
            new Alert("Ok", "Add command and bind this to viewmodel", "ok");
        }
    }
}