﻿using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputEntry : ContentView
    {

        
        public InputEntry()
        {
            InitializeComponent();

            PostEntry.Completed += (sender, args) => SubmitPostButton(sender, args);


        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            try
            {
                SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PostEntry.Text, "", "status", "", "", "", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                        if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                        {
                            HomepageViewModel.homePostList.Clear();
                            HomepageViewModel.LoadData();
                        }
                        else
                        {
                            MyProfileViewModel.profilePostList.Clear();
                            MyProfileViewModel.LoadData();
                        }
                        PostEntry.Text = string.Empty;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }


        public void PostStatus(object sender, EventArgs args)
        {

            Navigation.PushModalAsync(new PostStatusPage());
        }

        public void PostRequest(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new PostRequestPage());
        }
        public void PostSell(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new PostSellPage());
        }

        public  void AddStatusPost(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new PostStatusPage());
        }








        }
    }