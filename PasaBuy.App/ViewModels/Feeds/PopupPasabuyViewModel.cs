﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Views.Posts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Feeds
{
    public class PopupPasabuyViewModel : BaseViewModel
    {
        public ObservableCollection<PopupPasabuyModel> Info { get; set; }

        bool _isTapped = false;
        public bool IsTapped
        {
            get
            {
                return _isTapped;
            }
            set
            {
                if (_isTapped != value)
                {
                    _isTapped = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public PopupPasabuyViewModel()
        {
            TappedCommand = new Command<object>(TappedClicked);

            Info = new ObservableCollection<PopupPasabuyModel>();
            CheckMover();

            Info.Add(new PopupPasabuyModel() { Title = "Pabili", Details = "A buyer requests a mover to buy items for him in a restaurant or store. The mover delivers the item right after the purchase." });
            Info.Add(new PopupPasabuyModel() { Title = "Pahatid", Details = "A mover drops off a passenger at a destination." });
            Info.Add(new PopupPasabuyModel() { Title = "Pasakay", Details = "A mover invites a passenger to share a ride towards a destination." });
        }

        public void CheckMover()
        {
            try
            {
                Http.HatidPress.MoverData.Instance.GetData((bool success, string data) =>
                {
                    if (success)
                    {
                        Info.Add(new PopupPasabuyModel() { Title = "Pasabay", Details = "A mover posts items or his whereabouts and asks the community if someone wants to have the items bought by him. The mover will then deliver the items." });
                    }
                });
            }
            catch (Exception err)
            {
                if (Local.PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2MVR-P1PUPVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2MVR-P1PUPVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2MVR-P1PUPVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2MVR-P1PUPVM-" + err.ToString());
                }
            }
        }

        public Command<object> TappedCommand { get; set; }

        private async void TappedClicked(object obj)
        {
           
            var item = obj as PopupPasabuyModel;
            if (!IsTapped)
            {
                IsTapped = true;
                await PopupNavigation.Instance.PopAsync();
                if (item.Title == "Pasabay")
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new PasabayPage());
                }
                if (item.Title == "Pabili")
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new PabiliPage());
                }
                if (item.Title == "Pahatid")
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new PahatidPage());
                }
                if (item.Title == "Pasakay")
                {
                    //await App.Current.MainPage.Navigation.PushModalAsync(new PasakayPage());
                    await App.Current.MainPage.DisplayAlert("Notice", "This feature is not yet available", "Ok");

                }
                IsTapped = false;
            }

        }
    }
}
