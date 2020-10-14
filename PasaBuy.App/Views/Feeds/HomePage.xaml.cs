﻿using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Driver;
using Syncfusion.XForms.Buttons;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentView
    {
        public bool isBtn = false;
        public static int LastIndex = 11;
        public HomePage()
        {
            InitializeComponent();
            LastIndex = 11;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var classId = button.ClassId;
            await ShareUri(classId);
        }
        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri
            });
        }

        private void homeListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Post;
            if (HomepageViewModel.homePostList.Last() == item && HomepageViewModel.homePostList.Count() != 1)
            {
                if (HomepageViewModel.homePostList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    HomepageViewModel.LoadData(item.Last_ID);
                }
            }
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            if (!isBtn)
            {
                isBtn = true;
                isBusy.IsRunning = true;
                var btn = (SfButton)sender;
                var classId = btn.ClassId;
                if (classId != PSACache.Instance.UserInfo.wpid)
                {
                    MyProfileViewModel.GetProfile(classId);
                    MyProfileViewModel.LoadTotal(classId);
                    MyProfileViewModel.user_id = classId;

                    await Task.Delay(1000);
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
                    isBusy.IsRunning = false;
                    isBtn = false;
                    /*Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(500);
                    });*/
                }
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (!isBtn)
            {
                isBtn = true;
                isBusy.IsRunning = true;
                var btn = (ImageButton)sender;
                var classId = btn.ClassId;
                if (classId != PSACache.Instance.UserInfo.wpid)
                {
                    MyProfileViewModel.GetProfile(classId);
                    MyProfileViewModel.LoadTotal(classId);
                    MyProfileViewModel.user_id = classId;
                    await Task.Delay(1000);
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
                    isBusy.IsRunning = false;
                    isBtn = false;

                    /*Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Delay(500);
                    });*/
                }
            }
        }

        private void AcceptButton(object sender, EventArgs e)
        {
            try
            {
                if (!isBtn)
                {
                    isBtn = true;
                    var btn = (Button)sender;
                    SocioPress.Profile.Instance.GetData(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                    {
                        if (success)
                        {
                            UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data);
                            DriverChatMessageViewModel.ProfileNames = uinfo.data.dname;
                            DriverChatMessageViewModel.ProfileImages = PSAProc.GetUrl(uinfo.data.avatar);
                            DriverChatMessageViewModel.user_id = btn.ClassId;
                            DriverChatMessageViewModel.myPage = "home";
                            DriverChatMessageViewModel.refresh = 0;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await Task.Delay(200);
                                await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DriverChatMessagePage()));
                                isBtn = false;
                            });
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}