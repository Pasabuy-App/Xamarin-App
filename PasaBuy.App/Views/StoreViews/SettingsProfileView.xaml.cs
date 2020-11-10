﻿using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Settings;
using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsProfileView : ContentPage
    {
        public SettingsProfileView()
        {
            InitializeComponent();
            Avatar.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
            Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
            StoreInfo.Text = PSACache.Instance.UserInfo.store_info;
        }

        private async void AddLogo(object sender, EventArgs e)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Alert("Error", "No camera available", "Failed");
                    IsRunning.IsRunning = false;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
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

                Avatar.Source = imageSource;
                SaveImage(file.Path, "", "");
                //avatarUrl = file.Path;
            }
        }

        private async void AddBanner(object sender, EventArgs e)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Alert("Error", "No camera available", "Failed");
                    IsRunning.IsRunning = false;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
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

                Banner.Source = imageSource;
                SaveImage("", file.Path, "");
                //bannerUrl = file.Path;
            }
        }

        public void SaveImage(string avatar, string banner, string info)
        {
            try
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    Http.TindaPress.Store.Instance.Update(info, avatar, banner, (bool success, string data) =>
                    {
                        if (success)
                        {
                            EditProfile datas = JsonConvert.DeserializeObject<EditProfile>(data);
                            if (!string.IsNullOrEmpty(banner))
                            {
                                PSACache.Instance.UserInfo.store_banner = datas.data;
                                new Alert("Success", "Avatar successfully updated.", "OK");
                            }
                            if (!string.IsNullOrEmpty(avatar))
                            {
                                PSACache.Instance.UserInfo.store_logo = datas.data;
                                new Alert("Success", "Banner successfully updated.", "OK");
                            }
                            if (!string.IsNullOrEmpty(info))
                            {
                                PSACache.Instance.UserInfo.store_info = info;
                                new Alert("Success", "Information successfully updated.", "OK");;
                            }
                            Views.Navigation.MasterView.Insertimage(datas.data);
                            PSACache.Instance.SaveUserData();
                            IsRunning.IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning.IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-U1SPV.", "OK");
                IsRunning.IsRunning = false;
            }
        }

        private void AddInfo(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StoreInfo.Text))
            {
                SaveImage("", "", StoreInfo.Text);
            }
        }
    }
}