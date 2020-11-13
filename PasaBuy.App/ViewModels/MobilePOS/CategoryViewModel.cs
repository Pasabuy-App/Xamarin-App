using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class CategoryViewModel : BaseViewModel
    {
        #region Fields

        public static ObservableCollection<Models.TindaFeature.CategoryModel> categoriesList;
        public ObservableCollection<Models.TindaFeature.CategoryModel> CategoriesList
        {
            get
            {
                return categoriesList;
            }
            set
            {
                if (categoriesList != value)
                {
                    categoriesList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool isRunning = false;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
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

        #endregion
        public CategoryViewModel()
        {
            categoriesList = new ObservableCollection<Models.TindaFeature.CategoryModel>();
            LoadCategory();
            AddCommand = new Command<object>(AddClicked);
            UpdateCommand = new Command<object>(UpdateClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                LoadCategory();
                IsRefreshing = false;
            });
        }
        public Command<object> AddCommand { get; set; }

        private async void AddClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                PopupAddCategory.catid = "0";
                await PopupNavigation.Instance.PushAsync(new PopupAddCategory());
                IsRunning = false;
            }
        }

        public Command<object> UpdateCommand { get; set; }

        private async void UpdateClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var cat = obj as Models.TindaFeature.CategoryModel;
                PopupAddCategory.catid = cat.ID;
                PopupAddCategory.catname = cat.Title;
                PopupAddCategory.catinfo = cat.Info;
                await PopupNavigation.Instance.PushAsync(new PopupAddCategory());
                IsRunning = false;
            }
        }

        public void LoadCategory()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    categoriesList.Clear();
                    Http.TindaPress.Category.Instance.Listing("", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.CategoryModel category = JsonConvert.DeserializeObject<Models.TindaFeature.CategoryModel>(data);
                            for (int i = 0; i < category.data.Length; i++)
                            {
                                bool update = false;
                                bool delete = false;
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_category"))
                                {
                                    update = true;
                                }
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_category"))
                                {
                                    delete = true;
                                }
                                categoriesList.Add(new Models.TindaFeature.CategoryModel()
                                {
                                    ID = category.data[i].ID,
                                    Title = category.data[i].title,
                                    Info = category.data[i].info,
                                    isUpdate = update,
                                    isDelete = delete,
                                    isDeleteCol = update == true ? 1 : 0
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L1CVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshCategory(string search)
        {
            try
            {
                categoriesList.Clear();
                Http.TindaPress.Category.Instance.Listing("", search, "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.CategoryModel category = JsonConvert.DeserializeObject<Models.TindaFeature.CategoryModel>(data);
                        for (int i = 0; i < category.data.Length; i++)
                        {
                            bool update = false;
                            bool delete = false;
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_category"))
                            {
                                update = true;
                            }
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_category"))
                            {
                                delete = true;
                            }
                            categoriesList.Add(new Models.TindaFeature.CategoryModel()
                            {
                                ID = category.data[i].ID,
                                Title = category.data[i].title,
                                Info = category.data[i].info,
                                isUpdate = update,
                                isDelete = delete,
                                isDeleteCol = update == true ? 1 : 0
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L2CVM.", "OK");
            }
        }
    }
}
