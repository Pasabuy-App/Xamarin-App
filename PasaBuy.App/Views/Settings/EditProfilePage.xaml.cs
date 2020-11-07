using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Settings;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public string avatarUrl;
        public string bannerUrl;
        private bool isEnable = false;

        public EditProfilePage()
        {
            InitializeComponent();
            Avatar.IsEnabled = true;
            Banner.IsEnabled = true;
            //Fname.Text = PSACache.Instance.UserInfo.fname;
            //Lname.Text = PSACache.Instance.UserInfo.lname;
            //Nname.Text = PSACache.Instance.UserInfo.dname;
            Avatar.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
            Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.banner);
            

        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SaveAvatar(object sender, EventArgs e)
        {
            try
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    if (avatarUrl == null)
                    {
                        new Alert("Failed", "Please select an image for avatar first", "Ok");
                        IsRunning.IsRunning = false;
                        return;
                    }
                    else
                    {
                        Http.DataVice.Upload.Instance.Image(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, avatarUrl, "", "", "avatar", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                new Alert("Success", "Avatar successfully updated", "Ok");
                                EditProfile datas = JsonConvert.DeserializeObject<EditProfile>(data);
                                PSACache.Instance.UserInfo.avatar = datas.data;
                                PSACache.Instance.SaveUserData();
                                ViewModels.Feeds.HomepageViewModel.userinfoList.Clear();
                                ViewModels.Menu.MasterMenuViewModel.userinfoList.Clear();
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
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1UPL-I1EPP.", "OK");
                IsRunning.IsRunning = false;
            }
        }

        public void SaveBanner(object sender, EventArgs e)
        {
            try
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    if (bannerUrl == null)
                    {
                        new Alert("Failed", "Please select an image for banner first", "Ok");
                        IsRunning.IsRunning = false;
                        return;
                    }
                    else
                    {
                        Http.DataVice.Upload.Instance.Image(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, bannerUrl, "", "", "banner", "", (bool success, string data) =>
                        {
                            if (success)
                            {
                                new Alert("Success", "Banner successfully updated", "Ok");
                                EditProfile datas = JsonConvert.DeserializeObject<EditProfile>(data);
                                PSACache.Instance.UserInfo.banner = datas.data;
                                PSACache.Instance.SaveUserData();
                                ViewModels.Menu.MasterMenuViewModel.userinfoList.Clear();
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
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1UPL-I2EPP.", "OK");
                IsRunning.IsRunning = false;
            }
        }

        public async void SaveProfileData(object sender, EventArgs e)
        {
            try
            {
                if (!isEnable)
                {
                    isEnable = true;
                    /*Users.Instance.EditProfile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, Fname.Text, Lname.Text, Nname.Text, (bool success, string data) =>
                    {
                        if (success)
                        {
                            new Alert("Success", "Data successfully updated", "Ok");
                            PSACache.Instance.UserInfo.dname = Nname.Text;
                            PSACache.Instance.UserInfo.lname = Lname.Text;
                            PSACache.Instance.UserInfo.fname = Fname.Text;
                            PSACache.Instance.SaveUserData();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });*/
                    await Task.Delay(200);
                    isEnable = false;
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }



        async void AddAvatar(object sender, EventArgs args)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                Avatar.IsEnabled = false;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    new Alert("Error", "No camera available", "Failed");
                    IsRunning.IsRunning = false ;
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
                avatarUrl = file.Path;
                IsRunning.IsRunning = false;
            }
        }

        async void AddBanner(object sender, EventArgs args)
        {
            if (IsRunning.IsRunning == false)
            {
                IsRunning.IsRunning = true;
                Banner.IsEnabled = false;
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
                    IsRunning.IsRunning = false;
                    var stream = file.GetStream();
                    return stream;
                });

                Banner.Source = imageSource;
                bannerUrl = file.Path;
                IsRunning.IsRunning = false;
            }

        }
    }
}