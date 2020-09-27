﻿using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using Plugin.Media;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAddDocument : PopupPage
    {
        public string filepath = string.Empty;
        public PopupAddDocument()
        {
            InitializeComponent();
        }

        void RemoveDocumentImage(object sender, EventArgs args)
        {
            DocumentImage.Source = "Idcard.png";
        }

        async void OpenCameraCommand(object sender, EventArgs args)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Ok");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                CompressionQuality = 30
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
            string doctype = string.Empty;
            if (DocumentTypePicker.Text == "DTI Registration")
            {
                doctype = "dti_registration";
            }
            if (DocumentTypePicker.Text == "Barangay Clearance")
            {
                doctype = "barangay_clearance";
            }
            if (DocumentTypePicker.Text == "Lease Contract")
            {
                doctype = "lease_contract";
            }
            if (DocumentTypePicker.Text == "Community Tax")
            {
                doctype = "community_tax";
            }
            if (DocumentTypePicker.Text == "Occupancy Permit")
            {
                doctype = "occupancy_permit";
            }
            if (DocumentTypePicker.Text == "Sanitary Permit")
            {
                doctype = "sanitary_permit";
            }
            if (DocumentTypePicker.Text == "Fire Permit")
            {
                doctype = "fire_permit";
            }
            if (DocumentTypePicker.Text == "Mayor's Permit")
            {
                doctype = "mayors_permit";
            }
            if (filepath != string.Empty && DocumentTypePicker.Text != string.Empty)
            {
                //new Alert(DocumentTypePicker.Text, "Path: " + filepath, "OK");
                try
                {
                    TindaPress.Document.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, doctype, filepath,(bool success, string data) =>
                    {
                        if (success)
                        {
                            DocumentViewModel.documentList.Clear();
                            DocumentViewModel.LoadData();
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                        }
                    });
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
                }
            }
        }
    }
}