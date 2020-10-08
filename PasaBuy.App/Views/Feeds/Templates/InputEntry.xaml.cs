﻿using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.Posts;
using Rg.Plugins.Popup.Services;
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
        public bool isTapped;
        public InputEntry()
        {
            InitializeComponent();

            PostEntry.Completed += (sender, args) => SubmitPostButton(sender, args);
            isTapped = false;
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PostEntry.Text))
            {
                try
                {
                    SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PostEntry.Text, "", "status", "", "", "", "", "", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                            {
                                HomepageViewModel.homePostList.Clear();
                                HomepageViewModel.LoadData("");
                            }
                            else
                            {
                                MyProfileViewModel.profilePostList.Clear();
                                MyProfileViewModel.LoadData(PSACache.Instance.UserInfo.wpid);
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
            else
            {
                new Alert("Notice to user", "Please fill-up all fields.", "OK");
            }
        }


        public async void PostStatus(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostStatusPage());
                isTapped = false;
            }
        }

        public  async void PostRequest(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                //Navigation.PushModalAsync(new PostRequestPage());
                await PopupNavigation.Instance.PushAsync(new PopupPasabuy());
                isTapped = false;
            }
        }

     
        public async void PostSell(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostSellPage());
                isTapped = false;
            }
        }

        public async void AddStatusPost(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostStatusPage());
                isTapped = false;
            }
        }








        }
    }