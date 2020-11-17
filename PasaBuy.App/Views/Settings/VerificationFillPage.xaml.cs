﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFillPage : ContentPage
    {
        public static string idType = string.Empty;
        public static string idnumber = string.Empty;
        public static string idDocType = string.Empty;
        public static string idPath = string.Empty;
        public static string selfiePath = string.Empty;
        private bool isEnable = false;
        public VerificationFillPage()
        {
            InitializeComponent();
            Loader.IsVisible = false;
            IdTypeEntry.Text = idType;
            IDNumberEntry.Text = idnumber;
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //TO DO :: Add validations
                if (String.IsNullOrEmpty(IdTypeEntry.Text) || String.IsNullOrEmpty(IDNumberEntry.Text) || String.IsNullOrEmpty(ContactEntry.Text))
                {
                    new Alert("Failed", "Please complete all fields.", "Ok");
                }
                else
                {
                    if (!isEnable)
                    {
                        Loader.IsVisible = true;
                        isEnable = true;
                        Http.DataVice.Documents.Instance.Insert("id", idDocType, idPath, idnumber, "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                Http.DataVice.Documents.Instance.Insert("face", "", selfiePath, ContactEntry.Text, "", (bool success2, string data2) =>
                                {
                                    if (success)
                                    {
                                        Navigation.PushModalAsync(new VerificationFinal());
                                        Loader.IsVisible = false;
                                        isEnable = false;
                                    }
                                    else
                                    {
                                        isEnable = false;
                                        Loader.IsVisible = false;
                                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data2), "Try Again");
                                    }
                                });
                            }
                            else
                            {
                                isEnable = false;
                                Loader.IsVisible = false;
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                isEnable = false;
                Loader.IsVisible = false;
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}