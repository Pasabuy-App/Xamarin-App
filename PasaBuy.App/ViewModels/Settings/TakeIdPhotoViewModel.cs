﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Settings;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Settings
{
    public class TakeIdPhotoViewModel : BaseViewModel
    {
        public TakeIdPhotoViewModel()
        {
            this.UmidCommand = new Command(this.UmidClicked);
        }

        public Command UmidCommand { get; set; }

        private void UmidClicked(object sender)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new IdPhotoPage()));
            //await CrossMedia.Current.Initialize();
            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    new Alert("Error", "No camera available", "Failed");
            //}

            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "DocumentsFolder",
            //    SaveToAlbum = true,
            //    CompressionQuality = 40
            //});

            //if (file == null)
            //    return;

            //ImageSource imageSource = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});

            //var filePath = file.Path;


        }


    }
}