﻿using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using Plugin.Media;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditDocumentStore : PopupPage
    {
        private string filepath = string.Empty;
        public static string hash_id = string.Empty;

        public static string image = string.Empty;
        public static string doctype = string.Empty;
        public static string docid = string.Empty;

        public PopupEditDocumentStore()
        {
            InitializeComponent();
            this.BindingContext = new AddDocumentViewModel();
            DocumentImage.Source = image;
            DocumentTypePicker.Text = doctype;
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void OKModal_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (filepath != string.Empty && hash_id != string.Empty)
                {
                    if (IsRunning.IsRunning == false)
                    {
                        IsRunning.IsRunning = true;
                        Http.TindaPress.Document.Instance.Update(filepath, docid, hash_id, (bool success, string data) =>
                        {
                            if (success)
                            {
                                DocumentViewModel.RefreshData();
                                PopupNavigation.Instance.PopAsync();
                                IsRunning.IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning.IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-U1PUEDS", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-U1PUEDS-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-U1PUEDS.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-U1PUEDS-" + err.ToString());
                }
                IsRunning.IsRunning = false;
            }

        }

        void RemoveDocumentImage(object sender, EventArgs args)
        {
            DocumentImage.Source = "document.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Controllers.Notice.Alert("Error", "No camera available.", "OK");
                    IsRunning.IsRunning = false;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    CompressionQuality = 40
                });

                if (file == null)
                {
                    IsRunning.IsRunning = false;
                    return;
                }

                ImageSource imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    IsRunning.IsRunning = false;
                    return stream;
                });

                DocumentImage.Source = imageSource;
                //var filePath = file.Path;
                filepath = file.Path;
                IsRunning.IsRunning = false;
            }

        }

        async void BrowseGalleryCommand(object sender, EventArgs args)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Controllers.Notice.Alert("Error", "No camera available.", "OK");
                    IsRunning.IsRunning = false;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    SaveMetaData = false,
                    CompressionQuality = 30,
                    MaxWidthHeight = 1024
                });


                if (file == null)
                {
                    IsRunning.IsRunning = false;
                    return;
                }

                ImageSource imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    IsRunning.IsRunning = false;
                    return stream;
                });

                DocumentImage.Source = imageSource;
                filepath = file.Path;
                IsRunning.IsRunning = false;
            }

        }

        private void DocumentTypePicker_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            hash_id = DocumentTypePicker.SelectedValue.ToString();
        }
    }
}