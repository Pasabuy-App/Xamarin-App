using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class PartnerBrowserViewModel : BaseViewModel
    {
        public  ObservableCollection<PartnerStore> partnerStoresRotator;

        public static ObservableCollection<Categories> itemCategories;

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
            {   partnerStoresRotator = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public PartnerBrowserViewModel()
        {
            itemCategories = new ObservableCollection<Categories>();
            itemCategories.Clear();

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
                TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "all", "", "1", "1", (bool success, string data) =>
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
                                Avatar = datas.data[i].avatar, //PSAProc.GetUrl(datas.data[i].avatar),
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
