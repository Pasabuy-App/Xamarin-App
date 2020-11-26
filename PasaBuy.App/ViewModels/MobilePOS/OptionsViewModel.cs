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
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OptionsViewModel : BaseViewModel
    {
        private DelegateCommand _addOptionsCommand;
        public DelegateCommand AddOptionsCommand =>
          _addOptionsCommand ?? (_addOptionsCommand = new DelegateCommand(AddOptinsClicked));
        private async void AddOptinsClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                PopupEditOptions.option_id = string.Empty;
                await PopupNavigation.Instance.PushAsync(new PopupEditOptions());
                IsRunning = false;
            }
        }

        public static ObservableCollection<Models.TindaFeature.VariantModel> _optionsList;
        public ObservableCollection<Models.TindaFeature.VariantModel> OptionsList

        {
            get
            {
                return _optionsList;
            }
            set
            {
                _optionsList = value;
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

        public static string variant_id;
        public OptionsViewModel()
        {
            _optionsList = new ObservableCollection<Models.TindaFeature.VariantModel>();
            LoadOptions();

            RefreshCommand = new Command<string>((key) =>
            {
                LoadOptions();
                IsRefreshing = false;
            });

            DeleteCommand = new Command<object>(DeleteClicked);
            UpdateCommand = new Command<object>(UpdateClicked);
        }

        public Command<object> DeleteCommand { get; set; }

        private async void DeleteClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Option?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var variants = obj as Models.TindaFeature.VariantModel;
                        Http.TindaPress.Variant.Instance.Delete(variants.ID, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RefreshOptions();
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-D1OVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-D1OVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-D1OVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-D1OVM-" + err.ToString());
                }
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
                PopupEditOptions.option_id = variants.ID;
                PopupEditOptions.option_name = variants.Title;
                PopupEditOptions.option_info = variants.Info;
                PopupEditOptions.option_price = variants.Price.ToString();
                await PopupNavigation.Instance.PushAsync(new PopupEditOptions());
                IsRunning = false;
            }
        }

        public void LoadOptions()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _optionsList.Clear();
                    Http.TindaPress.Variant.Instance.Listing(ProductVariants.product_id, variant_id, "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.VariantModel variants = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                            for (int i = 0; i < 1; i++)
                            {
                                for (int ii = 0; ii < variants.data[i].options.Count; ii++)
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
                                    _optionsList.Add(new Models.TindaFeature.VariantModel()
                                    {
                                        ID = variants.data[i].options[ii].ID,
                                        Title = variants.data[i].options[ii].name,
                                        Info = variants.data[i].options[ii].info,
                                        Price = Convert.ToDouble(variants.data[i].options[ii].price),
                                        isUpdate = update,
                                        isDelete = delete,
                                        isDeleteCol = update == true ? 1 : 0
                                    });
                                }
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-L1OVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-L1OVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L1OVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-L1OVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

        public static void RefreshOptions()
        {
            try
            {
                _optionsList.Clear();
                Http.TindaPress.Variant.Instance.Listing(ProductVariants.product_id, variant_id, "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.VariantModel variants = JsonConvert.DeserializeObject<Models.TindaFeature.VariantModel>(data);

                        for (int i = 0; i < 1; i++)
                        {
                            for (int ii = 0; ii < variants.data[i].options.Count; ii++)
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
                                _optionsList.Add(new Models.TindaFeature.VariantModel()
                                {
                                    ID = variants.data[i].options[ii].ID,
                                    Title = variants.data[i].options[ii].name,
                                    Info = variants.data[i].options[ii].info,
                                    Price = Convert.ToDouble(variants.data[i].options[ii].price),
                                    isUpdate = update,
                                    isDelete = delete,
                                    isDeleteCol = update == true ? 1 : 0
                                });
                            }
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2VRT-L1OVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2VRT-L1OVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2VRT-L1OVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2VRT-L1OVM-" + err.ToString());
                }
            }
        }
    }
}
