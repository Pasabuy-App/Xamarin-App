﻿using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Views.Master;
using System;

namespace PasaBuy.App.Views.Settings
{
    /// <summary>
    /// Page to show the setting.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingPage" /> class.
        /// </summary>
        public SettingPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new SettingPage());
        }

        /// <summary>
        /// Invoked when option button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void OptionButton_Clicked(object sender, EventArgs e)
        {
            //Do something...
        }
    }
}