﻿using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Management
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonnelsView : ContentPage
    {
        public int count = 0;
        public PersonnelsView()
        {
            InitializeComponent();
            this.BindingContext = new PersonnelsViewModel();
            AddButton.IsEnabled = false;
            CheckPermission();
        }

        public void CheckPermission()
        {
            bool edit = false;
            bool delete = false;
            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_personnel")
                {
                    edit = true;
                }
                if (per.action == "delete_personnel")
                {
                    delete = true;
                }
            }
            if (!delete && !edit)
            {
                listView.AllowSwiping = false;
            }
            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "add_personnel"))
            {
                AddButton.IsEnabled = true;
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void AddTapped(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PushAsync(new PopupAddPersonnel());
            AddButton.IsEnabled = false;
            await Navigation.PushModalAsync(new AddPersonnelView());
            AddButton.IsEnabled = true;

        }

        /*private async void Update_Tapped(object sender, EventArgs e)
        {
            var btn = sender as Grid; //  btn.ClassId
            await PopupNavigation.Instance.PushAsync(new PopupEditPersonnel());
        }*/

        /*private async void Delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (count == 0)
                {
                    count = 1;
                    bool answer = await DisplayAlert("Delete Address?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var btn = sender as Grid;
                        TindaPress.Personnel.Instance.Delete(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, btn.ClassId, (bool success, string data) =>
                        {
                            if (success)
                            {
                                PersonnelsViewModel._personnelsList.Clear();
                                PersonnelsViewModel.LoadData();
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                    await Task.Delay(200);
                    count = 0;
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }*/
    }
}