﻿using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        public CategoryView()
        {
            InitializeComponent();
            AddCategoryButton.Clicked += AddCategoryClicked;
        }

        private void AddCategoryClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAddCategory());
        }

    }
}