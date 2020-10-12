using System;
using System.Collections.Generic;
using PasaBuy.App.Controllers.Notice;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.Local;
using Plugin.Media;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Models.Settings;

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
        }

        private async void AddLogo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            Avatar.Source = imageSource;
            SaveImage(file.Path, "logo");
            //avatarUrl = file.Path;
        }

        private async void AddBanner(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                new Alert("Error", "No camera available", "Failed");
            }

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            ImageSource imageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            Banner.Source = imageSource;
            SaveImage(file.Path, "banner");
            //bannerUrl = file.Path;
        }
        public void SaveImage(string filepath, string type)
        {
            try
            {
                Upload.Instance.Image(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, filepath, PSACache.Instance.UserInfo.stid, "", type, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        EditProfile datas = JsonConvert.DeserializeObject<EditProfile>(data);
                        if (type == "banner")
                        {
                            PSACache.Instance.UserInfo.store_banner = datas.data;
                        }
                        if (type == "logo")
                        {
                            PSACache.Instance.UserInfo.store_logo = datas.data;
                        }
                        Views.Navigation.MasterView.Insertimage(datas.data);
                        PSACache.Instance.SaveUserData();
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}