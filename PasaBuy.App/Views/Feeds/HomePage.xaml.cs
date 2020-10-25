using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Controls;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Driver;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using System;
using System.Linq;
using System.Reflection;
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
        public static int LastIndex;

        ScrollAxisBase scrollRows;
        bool isAlertShown = false;
        public static bool isFirstLoad = false;
        public HomePage()
        {
            InitializeComponent();
            VisualContainer visualContainer = listView.GetType().GetRuntimeProperties().First(p => p.Name == "VisualContainer").GetValue(listView) as VisualContainer;
            scrollRows = visualContainer.GetType().GetRuntimeProperties().First(p => p.Name == "ScrollRows").GetValue(visualContainer) as ScrollAxisBase;
            scrollRows.Changed += ScrollRows_Changed;

            LastIndex = 12;

        }
        private void ScrollRows_Changed(object sender, ScrollChangedEventArgs e)
        {
            var lastIndex = scrollRows.LastBodyVisibleLineIndex;

            //To include header if used
            var header = (listView.HeaderTemplate != null && !listView.IsStickyHeader) ? 1 : 0;

            //To include footer if used
            var footer = (listView.FooterTemplate != null && !listView.IsStickyFooter) ? 1 : 0;
            var totalItems = listView.DataSource.DisplayItems.Count + header + footer;

            if ((lastIndex == totalItems - 1))
            {
                if (!isAlertShown)
                {
                    if (isFirstLoad)
                    {
                        //new Alert("Alert", "End of list reached...", "Ok");
                        HomepageViewModel.LoadData(LastIndex.ToString());
                        LastIndex += 7;
                    }
                    else
                    {
                        isFirstLoad = true;
                    }
                    isAlertShown = true;
                }
            }
            else
                isAlertShown = false;
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
            /*var item = e.ItemData as Post;
            if (HomepageViewModel.homePostList.Last() == item && HomepageViewModel.homePostList.Count() != 1)
            {
                if (HomepageViewModel.homePostList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    new Alert("OK", "OK", "OK");
                    //HomepageViewModel.LoadData(item.Last_ID);
                }
            }*/
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            if (!isBtn)
            {
                isBtn = true;
                var btn = (SfButton)sender;
                var classId = btn.ClassId;
                if (classId != PSACache.Instance.UserInfo.wpid)
                {
                    MyProfileViewModel.GetProfile(classId);
                    MyProfileViewModel.LoadTotal(classId);
                    MyProfileViewModel.user_id = classId;

                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
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
                var btn = (ImageButton)sender;
                var classId = btn.ClassId;
                if (classId != PSACache.Instance.UserInfo.wpid)
                {
                    MyProfileViewModel.GetProfile(classId);
                    MyProfileViewModel.LoadTotal(classId);
                    MyProfileViewModel.user_id = classId;
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
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