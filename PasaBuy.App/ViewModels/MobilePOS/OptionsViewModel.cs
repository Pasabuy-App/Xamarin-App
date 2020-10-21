using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class OptionsViewModel : BaseViewModel
    {
        public static string variant_id;
        private DelegateCommand _addOptionsCommand;
        public DelegateCommand AddOptionsCommand =>
          _addOptionsCommand ?? (_addOptionsCommand = new DelegateCommand(AddOptinsClicked));
        private async void AddOptinsClicked(object obj)
        {
            //PopupAddVariants.option_id = string.Empty;
            //new Alert("Variants to Options", " Click Add Model " + PopupAddVariants.type + " " + ProductVariants.product_id, "OK");

            //PopupAddVariants.type = string.Empty;
            //new Alert("Variants to Options", " Click Add Model", "OK");
            await PopupNavigation.Instance.PushAsync(new PopupEditOptions());
        }

        public static ObservableCollection<Options> _optionsList;
        public ObservableCollection<Options> OptionsList

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
        public OptionsViewModel()
        {
            _optionsList = new ObservableCollection<Options>();

            LoadOptions(ProductVariants.product_id, variant_id);
        }
        public static void LoadOptions(string product_id, string variant_id)
        {
            try
            {
                _optionsList.Clear();
                TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, product_id, variant_id, "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Options options = JsonConvert.DeserializeObject<Options>(data);
                        if (options.data.Length > 0)
                        {
                            for (int i = 0; i < options.data.Length; i++)
                            {
                                _optionsList.Add(new Options()
                                {
                                    Id = options.data[i].ID,
                                    Name = options.data[i].name,
                                    Price = Convert.ToDouble(options.data[i].price)
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
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
    }
}
