﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFillPage : ContentPage
    {
        public static string idType = string.Empty;
        public static string idDocType = string.Empty;
        public static string idPath = string.Empty;
        public static string selfiePath = string.Empty;
        private bool isEnable = false;
        public VerificationFillPage()
        {
            InitializeComponent();
            IdTypeEntry.Text = idType;
            LastNameEntry.Text = PSACache.Instance.UserInfo.lname;
            FirstNameEntry.Text = PSACache.Instance.UserInfo.fname;
        }
        private void NextButtonClicked(object sender, EventArgs e)
        {   
            //TO DO :: Add validations
            if (String.IsNullOrEmpty(IdTypeEntry.Text) || String.IsNullOrEmpty(IDNumberEntry.Text) || String.IsNullOrEmpty(LastNameEntry.Text) 
                || String.IsNullOrEmpty(FirstNameEntry.Text) || String.IsNullOrEmpty(NationalityEntry.Text) )
            {   
                new Alert("Failed", "Please complete all fields.", "Ok");
            }
            else
            {
                if (!isEnable)
                {
                    isEnable = true;
                    //Save this to database

                    Navigation.PushModalAsync(new VerificationFinal());
                }
            }
        }
    }
}