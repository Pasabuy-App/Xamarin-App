using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class MyStoreListViewModel : BaseViewModel
    {
        public ObservableCollection<Store> myStores;

        public ObservableCollection<Store> MyStores
        {
            get
            {
                return myStores;
            }
            set
            {
                myStores = value;
                this.NotifyPropertyChanged();
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


        public MyStoreListViewModel()
        {
            myStores = new ObservableCollection<Store>();
            LoadStore();
            RefreshCommand = new Command<string>((key) =>
            {
                myStores.Clear();
                LoadStore();
                IsRefreshing = false;
            });
        }
        public void LoadStore()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    TindaPress.Personnel.Instance.Store_List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                myStores.Add(new Store()
                                {
                                    Id = datas.data[i].ID,
                                    //RoleID = datas.data[i].roid,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" || string.IsNullOrEmpty(datas.data[i].avatar) ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                    Banner = datas.data[i].banner == "None" || string.IsNullOrEmpty(datas.data[i].banner) ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner)
                                });
                            }
                            IsRunning = false ;
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
                Views.Navigation.MasterView.MyType = "store";
                PSACache.Instance.UserInfo.stid = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).Id;
                PSACache.Instance.UserInfo.store_name = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).Title;
                //PSACache.Instance.UserInfo.roid = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).roid;
                PSACache.Instance.UserInfo.store_logo = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).Logo;
                PSACache.Instance.UserInfo.store_banner = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Store).Banner;
                await Task.Delay(300);
                App.Current.MainPage = new Views.Navigation.NavigationView();
                await Task.Delay(300);
                IsRunning = false;
            }
        }
    }
}
