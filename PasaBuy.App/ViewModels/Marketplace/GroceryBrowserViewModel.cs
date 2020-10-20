﻿using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class GroceryBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        public static ObservableCollection<Groceries> grocerystorelist;

        private static GroceryBrowserViewModel instance;
        public static GroceryBrowserViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GroceryBrowserViewModel();
                }
                return instance;
            }

        }


        public ObservableCollection<Groceries> Grocerystorelist
        {
            get
            {
                return grocerystorelist;
            }
            set
            {
                grocerystorelist = value;
                this.NotifyPropertyChanged();
            }
        }
        public GroceryBrowserViewModel()
        {
            grocerystorelist = new ObservableCollection<Groceries>();
            grocerystorelist.Clear();
            //LoadGrocery("");
        }

        public static void LoadGrocery(string lastid)
        {
            try
            {
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "2", "", "1", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        GroceriesStoreListData datas = JsonConvert.DeserializeObject<GroceriesStoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            grocerystorelist.Add(new Groceries()
                            {
                                Id = datas.data[i].ID,
                                Title = datas.data[i].title,
                                Description = datas.data[i].short_info,
                                Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(datas.data[i].avatar), // "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png"
                                Offer = "50% off",
                                ItemRating = "4.5",
                                Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner), //https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png
                                Street = datas.data[i].street + " " + datas.data[i].brgy + " " + datas.data[i].city + " " + datas.data[i].province + ", " + datas.data[i].country //"#4 Rainbow Ave Pacita 2 San Pedro City, Laguna"
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public static void SearchStore(string search)
        {
            try
            {
                TindaPress.Store.Instance.Search(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, search, (bool success, string data) =>
                {
                    if (success)
                    {
                        GroceriesStoreListData datas = JsonConvert.DeserializeObject<GroceriesStoreListData>(data);
                        if (datas.data.Length > 0)
                        {
                            grocerystorelist.Clear();
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                grocerystorelist.Add(new Groceries()
                                {
                                    Id = datas.data[i].ID,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(datas.data[i].avatar), // "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png"
                                    Offer = "50% off",
                                    ItemRating = "4.5",
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner), //https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png
                                    Street = datas.data[i].street + " " + datas.data[i].brgy + " " + datas.data[i].city + " " + datas.data[i].province + ", " + datas.data[i].country //"#4 Rainbow Ave Pacita 2 San Pedro City, Laguna"
                                });
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", "No store found.", "Try Again");
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public Command RefreshCommand { protected set; get; }
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        private void NavigateToNextPage(object selectedItem)
        {
            //var item = selectedItem.ItemData as FoodStore;

            //new Alert("ok", "." +".HAHAHA", "ok");
        }

        //public ObservableCollection<Groceries> NavigationList { get; set; }
    }
}
