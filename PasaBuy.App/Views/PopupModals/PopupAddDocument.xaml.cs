using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Navigation;
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
            List<string> list = new List<string>();
            if (MasterView.MyType == "store")
            {
                list.Add("DTI Registration");
                list.Add("Barangay Clearance");
                list.Add("Lease Contract");
                list.Add("Community Tax");
                list.Add("Occupancy Permit");
                list.Add("Sanitary Permit");
                list.Add("Fire Permit");
                list.Add("Mayor's Permit");
            }
            if (MasterView.MyType == "mover")
            {
                list.Add("License Card");
                list.Add("License OR");
                list.Add("Vehicle CR");
                list.Add("Vehicle OR");
                list.Add("Vehicle's Front");
                list.Add("Vehicle's Right");
                list.Add("Vehicle's Left");
                list.Add("Vehicle's Back");
            }
            DocumentTypePicker.DataSource = list;
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
            if (filepath != string.Empty && DocumentTypePicker.Text != string.Empty)
            {
                //new Alert(DocumentTypePicker.Text, "Path: " + filepath, "OK");
                try
                {
                    string doctype = string.Empty;
                    if (MasterView.MyType == "store")
                    {
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
                        TindaPress.Document.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, doctype, filepath, (bool success, string data) =>
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
                    if (MasterView.MyType == "mover")
                    {
                        if (DocumentTypePicker.Text == "Vehicle's Back")
                        {
                            doctype = "vehicle_back";
                        }
                        if (DocumentTypePicker.Text == "Vehicle's Left")
                        {
                            doctype = "vehicle_left";
                        }
                        if (DocumentTypePicker.Text == "Vehicle's Right")
                        {
                            doctype = "vehicle_right";
                        }
                        if (DocumentTypePicker.Text == "Vehicle's Front")
                        {
                            doctype = "vehicle_front";
                        }
                        if (DocumentTypePicker.Text == "Vehicle CR")
                        {
                            doctype = "vehicle_cr";
                        }
                        if (DocumentTypePicker.Text == "Vehicle OR")
                        {
                            doctype = "vehicle_or";
                        }
                        if (DocumentTypePicker.Text == "License OR")
                        {
                            doctype = "license_or";
                        }
                        if (DocumentTypePicker.Text == "License Card")
                        {
                            doctype = "license_card";
                        }
                        HatidPress.Documents.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, filepath, doctype, PSACache.Instance.UserInfo.stid, "", (bool success, string data) =>
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
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
                }
            }
        }
    }
}