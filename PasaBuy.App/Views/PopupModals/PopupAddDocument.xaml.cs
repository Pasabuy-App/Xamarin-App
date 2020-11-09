﻿using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
using Plugin.Media;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddDocument : PopupPage
    {
        private string filepath = string.Empty;
        private string hash_id = string.Empty;
        public PopupAddDocument()
        {
            InitializeComponent();
            this.BindingContext = new AddDocumentViewModel();
        }

        void RemoveDocumentImage(object sender, EventArgs args)
        {
            DocumentImage.Source = "document.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                CompressionQuality = 40
            });

            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            DocumentImage.Source = imageSource;
            //var filePath = file.Path;
            filepath = file.Path;

        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                SaveMetaData = false,
                CompressionQuality = 30,
                MaxWidthHeight = 1024
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            DocumentImage.Source = imageSource;
            filepath = file.Path;

        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OKModal_Clicked(object sender, EventArgs e)
        {
            //new Alert(DocumentTypePicker.Text, "Path: " + filepath, "OK");
            try
            {
                if (filepath != string.Empty && hash_id != string.Empty)
                {
                    Http.TindaPress.Document.Instance.Insert(filepath, PSACache.Instance.UserInfo.stid, "", hash_id, (bool success, string data) =>
                    {
                        if (success)
                        {
                            DocumentViewModel.RefreshData();
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2DOC-I1PUAD.", "OK");
            }
        }

        private void DocumentTypePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            hash_id = DocumentTypePicker.SelectedValue.ToString();
        }
    }
}