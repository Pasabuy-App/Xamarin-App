﻿using PasaBuy.App.ViewModels.Driver;
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
    public partial class PopupSelectSchedule : PopupPage
    {
        public PopupSelectSchedule()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
            this.BindingContext = new DriverScheduleViewModel();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}