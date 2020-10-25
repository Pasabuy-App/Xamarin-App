using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Marketplace
{
    /// <summary>
    /// ViewModel for the Restaurant list .
    /// </summary>
    [Preserve(AllMembers = true)]
    //[DataContract]
    public class StoreBrowserViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private int? cartItemCount;

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
        public StoreBrowserViewModel()
        {
            itemCategories = new ObservableCollection<Categories>();
            itemCategories.Clear();
            LoadCategory();
            RefreshCommand = new Command<string>((key) =>
            {
                itemCategories.Clear();
                LoadCategory();
                IsRefreshing = false;
            });
        }

        public void LoadCategory()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
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
                            IsRunning = false;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref=StoreBrowserViewModel"/> class.
        /// </summary>

        public int? CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties

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
        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemSelectedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the navigation list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            CheckStore(((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Categories).Id, ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as Categories).Title);
        }

        public void CheckStore(string catid, string title)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, "", "1", "", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                            if (datas.data.Length > 0)
                            {
                                Views.Marketplace.StoreListPage.catid = catid;
                                Views.Marketplace.StoreListPage.pageTitle = title;
                                StoreListViewModel.LoadStore(catid, "");
                                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.StoreListPage());
                                IsRunning = false;
                            }
                            else
                            {
                                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NoStoresPage()));
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

        #endregion
    }
}
