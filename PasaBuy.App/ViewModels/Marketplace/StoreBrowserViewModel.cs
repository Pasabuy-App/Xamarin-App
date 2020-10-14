using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
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

        public StoreBrowserViewModel()
        {
            RefreshCommand = new Command<string>((key) =>
            {
                itemCategories.Clear();
                LoadCategory();
                IsRefreshing = false;
            });
            itemCategories = new ObservableCollection<Categories>();
            itemCategories.Clear();
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
        public Command<object> ItemTappedCommand
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
        }


        #endregion
    }
}
