using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class PartnerBrowserViewModel : BaseViewModel
    {
        public static ObservableCollection<PartnerStore> partnerStoresRotator;

        public static ObservableCollection<Store> storeList;

        public static ObservableCollection<Categories> itemCategories;

        public ObservableCollection<Store> StoreList
        {
            get
            {
                return storeList;
            }
            set
            {
                storeList = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Categories> ItemCategories
        {
            get
            {
                return itemCategories;
            }
            set
            {
                itemCategories = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<PartnerStore> PartnerStoresRotator
        {
            get
            {
                return partnerStoresRotator;
            }
            set
            {
                partnerStoresRotator = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ICommand RefreshCommand { protected set; get; }

        public PartnerBrowserViewModel()
        {
            RefreshCommand = new Command<string>((key) =>
            {
                storeList.Clear();
                partnerStoresRotator.Clear();
                LoadPartner();
                LoadRotator();
                IsRefreshing = false;
            });
            storeList = new ObservableCollection<Store>();
            storeList.Clear();
            partnerStoresRotator = new ObservableCollection<PartnerStore>();
            partnerStoresRotator.Clear();
            LoadPartner();
            LoadRotator();
        }
        public void LoadPartner()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "robinson", "", "1", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                            if (datas.data.Length > 0)
                            {
                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    storeList.Add(new Store()
                                    {
                                        Id = datas.data[i].ID,
                                        Title = datas.data[i].title,
                                        Description = datas.data[i].short_info,
                                        Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                        Offer = "50% off",
                                        ItemRating = "4.5",
                                        Street = datas.data[i].brgy + " " + datas.data[i].city
                                    });
                                }
                                IsRunning = false;
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsRunning = false;
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
                            storeList.Clear();
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                storeList.Add(new Store()
                                {
                                    Id = datas.data[i].ID,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                    Offer = "50% off",
                                    ItemRating = "4.5",
                                    Street = datas.data[i].brgy + " " + datas.data[i].city
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

        public static void LoadCategory()
        {
            try
            {
                TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "robinson", "", "1", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            itemCategories.Add(new Categories()
                            {
                                Id = datas.data[i].ID,
                                Title = datas.data[i].title,
                                Avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png" : PSAProc.GetUrl(datas.data[i].avatar), //PSAProc.GetUrl(datas.data[i].avatar),
                                Info = "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png"
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

        public static void LoadRotator()
        {
            try
            {
                partnerStoresRotator.Add(new PartnerStore()
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/Rob1.jpg"
                });
                partnerStoresRotator.Add(new PartnerStore()
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/rob2.jpg"
                });
                partnerStoresRotator.Add(new PartnerStore()
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/DeliveryVan.png"
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                StoreDetailsViewModel.store_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).Id;

                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await Task.Delay(300);
                IsRunning = false;
            }
        }
    }
}
