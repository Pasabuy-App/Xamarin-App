﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFinal : ContentPage
    {
        public VerificationFinal()
        {
            InitializeComponent();
        }

        private void BackToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainTabs();
        }
    }
}