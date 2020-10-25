﻿using PasaBuy.App.ViewModels.MobilePOS;
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
    public partial class PopupEditRole : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PopupEditRole()
        {
            InitializeComponent();
            this.BindingContext = new RolesViewModel();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}