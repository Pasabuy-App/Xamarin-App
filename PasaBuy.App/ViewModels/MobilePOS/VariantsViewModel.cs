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
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class VariantsViewModel : BaseViewModel
    {
        public static ObservableCollection<Variants> _variantsList;

        public static ObservableCollection<Options> _optionsList;

        private DelegateCommand _addVariantsCommand;

        public DelegateCommand AddVariantsCommand =>
          _addVariantsCommand ?? (_addVariantsCommand = new DelegateCommand(AddVariantsClicked));

        public ICommand ShowOptionsCommand
        {
            get
            {
                return new Command<string>((x) => ShowOptions(x));
            }
        }

        private async void ShowOptions(string id)
        {
            //new Alert("Variants to Options", "." + id + ". ." + ProductVariants.product_id + ".", "OK");
            OptionsView.variant_id = id;
            _optionsList.Clear();
            LoadOptions(ProductVariants.product_id, id);
            PopupAddVariants.type = "options";
            await Application.Current.MainPage.Navigation.PushModalAsync(new OptionsView());
        }

        private async void AddVariantsClicked(object obj)
        {
            PopupAddVariants.variant_id = string.Empty;
            PopupAddVariants.option_id = string.Empty;
            //PopupAddVariants.type = string.Empty;
            //new Alert("Variants to Options", " Click Add Model", "OK");
            await PopupNavigation.Instance.PushAsync(new PopupAddVariants());
        }

        public ObservableCollection<Variants> VariantsList
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

        public VariantsViewModel()
        {
            _variantsList = new ObservableCollection<Variants>();
            _optionsList = new ObservableCollection<Options>();

        }
        public static void LoadVariants(string product_id)
        {
            try
            {
                TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, product_id, "0", "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                        if (variants.data.Length > 0)
                        {
                            for (int i = 0; i < variants.data.Length; i++)
                            {
                                _variantsList.Add(new Variants()
                                {
                                    Id = variants.data[i].ID,
                                    Name = variants.data[i].name
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
            /* _variantsList.Add(new Variants()
             {
                 Name = "Size",
                 Id = "23"
             });
             _variantsList.Add(new Variants()
             {
                 Name = "Flavor",
                 Id = "42"
             });*/
        }

        public static void LoadOptions(string product_id, string variant_id)
        {
            try
            {
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
                                    Name = options.data[i].name
                                });
                            }
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });

                /*_optionsList.Add(new Options()
                {
                    Name = "Option 1",
                    Id = "123"
                });
                _optionsList.Add(new Options()
                {
                    Name = "Option 2",
                    Id = "2"
                });
                _optionsList.Add(new Options()
                {
                    Name = "Option 3",
                    Id = "15"
                });*/
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
    }
}