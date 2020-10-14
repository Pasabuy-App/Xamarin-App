using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class PartnerBrowserViewModel : BaseViewModel
    {
        public ObservableCollection<PartnerStore> partnerStoresRotator;

        public ObservableCollection<Store> storeList;

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

        public ICommand RefreshCommand { protected set; get; }

        public PartnerBrowserViewModel()
        {
            this.StoreList = new ObservableCollection<Store>()
            {
                new Store
                {
                    Title = "Store na Malakas",
                    Street = "#Rainbow Ave, San Pedro, Laguna",
                    Logo = "House.png",
                    Description = "NA"
                },
                new Store
                {
                    Title = "Store na Store",
                    Street = "#23 Sunflower St, Putatan, Muntinlupa",
                    Logo = "House.png",
                    Description = "NA"
                },
            };

            this.PartnerStoresRotator = new ObservableCollection<PartnerStore>()
            {
                new PartnerStore
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                },
                new PartnerStore
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg"
                },
                new PartnerStore
                {
                    Image = "https://pasabuy.app/wp-content/uploads/2020/10/DeliveryVan.png"
                }
            };
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
    }
}
