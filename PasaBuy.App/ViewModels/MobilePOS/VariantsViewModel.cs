using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class VariantsViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.TindaFeature.VariantModel> _variantsList;

        public ObservableCollection<Models.TindaFeature.VariantModel> VariantsList
        {
            get
            {
                return _variantsList;
            }
            set
            {
                _variantsList = value;
                this.NotifyPropertyChanged();
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
        public static string product_id;

        public VariantsViewModel()
        {
            _variantsList = new ObservableCollection<Models.TindaFeature.VariantModel>();
            LoadVariants();
            ShowOptionsCommand = new Command<object>(ShowOptions);
            UpdateCommand = new Command<object>(UpdateClicked);
            DeleteCommand = new Command<object>(DeleteClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                LoadVariants();
                IsRefreshing = false;
            });
        }

        public Command<object> DeleteCommand { get; set; }

        private async void DeleteClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Variant?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var variants = obj as Models.TindaFeature.VariantModel;
                        Http.TindaPress.Variant.Instance.Delete(variants.ID, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RefreshVariants();
                                IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning = false;
                            }
                        });
                    }
                    else
                    {
                        IsRunning = false;
                    }
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2VRT-D1VVM.", "OK");
                IsRunning = false;
            }
        }

        public Command<object> UpdateCommand { get; set; }

        private async void UpdateClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var variants = obj as Models.TindaFeature.VariantModel;
                PopupEditVariants.variant_id = variants.ID;
                PopupEditVariants.variant_name = variants.Title;
                PopupEditVariants.variant_info = variants.Info;
                PopupEditVariants.variant_required = variants.Required == "Required: Yes" ? "true" : "false";
                await PopupNavigation.Instance.PushAsync(new PopupEditVariants());
                IsRunning = false;
            }
        }

        private DelegateCommand _addVariantsCommand;

        public DelegateCommand AddVariantsCommand =>
          _addVariantsCommand ?? (_addVariantsCommand = new DelegateCommand(AddVariantsClicked));

        public Command<object> ShowOptionsCommand { get; set; }

        private async void ShowOptions(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var variants = obj as Models.TindaFeature.VariantModel;
                OptionsViewModel.variant_id = variants.ID;
                OptionsView.title = variants.Title;
                await Application.Current.MainPage.Navigation.PushModalAsync(new OptionsView());
                IsRunning = false;
            }
        }

        private async void AddVariantsClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                await PopupNavigation.Instance.PushAsync(new PopupAddVariants());
                IsRunning = false;
            }
        }

        public void LoadVariants()
        {
            try
            {
                if (!IsRunning) 
                {
                    IsRunning = true;
                    _variantsList.Clear();
                    Http.TindaPress.Variant.Instance.Listing(product_id, "", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.VariantModel variants = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                            for (int i = 0; i < variants.data.Length; i++)
                            {
                                bool update = false;
                                bool delete = false;
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_variant"))
                                {
                                    update = true;
                                }
                                if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_variant"))
                                {
                                    delete = true;
                                }
                                string required = variants.data[i].required == "true" ? "Yes" : "No";
                                _variantsList.Add(new Models.TindaFeature.VariantModel()
                                {
                                    ID = variants.data[i].ID,
                                    Title = variants.data[i].title,
                                    Info = variants.data[i].info,
                                    Required = "Required: " + required,
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L1VVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshVariants()
        {
            try
            {
                _variantsList.Clear();
                Http.TindaPress.Variant.Instance.Listing(product_id, "", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.VariantModel variants = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                        for (int i = 0; i < variants.data.Length; i++)
                        {
                            bool update = false;
                            bool delete = false;
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "edit_variant"))
                            {
                                update = true;
                            }
                            if (ViewModels.MobilePOS.MyStoreListViewModel.permissions.Any(p => p.action == "delete_variant"))
                            {
                                delete = true;
                            }
                            string required = variants.data[i].required == "true" ? "Yes" : "No";
                            _variantsList.Add(new Models.TindaFeature.VariantModel()
                            {
                                ID = variants.data[i].ID,
                                Title = variants.data[i].title,
                                Info = variants.data[i].info,
                                Required = "Required: " + required,
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
                new Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L2VVM.", "OK");
            }
        }
    }
}