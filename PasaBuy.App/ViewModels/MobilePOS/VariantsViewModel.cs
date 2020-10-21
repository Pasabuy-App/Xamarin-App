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

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class VariantsViewModel : BaseViewModel
    {
        public static ObservableCollection<Variants> _variantsList;

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

        public bool isClicked;
        private async void ShowOptions(string id)
        {
            if (!isClicked)
            {
                isClicked = true;
                //new Alert("Variants to Options", "." + id + ". ." + ProductVariants.product_id + ".", "OK");
                //OptionsView.variant_id = id;
                //_optionsList.Clear();
                OptionsViewModel.variant_id = id;
                //PopupAddVariants.type = "options";
                //new Alert("Variants to Options", " Click Add Model " + id + " " + ProductVariants.product_id, "OK");
                await Application.Current.MainPage.Navigation.PushModalAsync(new OptionsView());
                await Task.Delay(200);
                isClicked = false;
            }
        }

        private async void AddVariantsClicked(object obj)
        {
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

        public VariantsViewModel()
        {
            isClicked = false;
            _variantsList = new ObservableCollection<Variants>();
        }
        public static void LoadVariants(string product_id)
        {
            try
            {
                _variantsList.Clear();
                TindaPress.Variant.Instance.Listing(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, product_id, "0", "1", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Variants variants = JsonConvert.DeserializeObject<Variants>(data);
                        Console.WriteLine(data);
                        if (variants.data.Length > 0)
                        {
                            for (int i = 0; i < variants.data.Length; i++)
                            {
                                _variantsList.Add(new Variants()
                                {
                                    Id = variants.data[i].ID,
                                    Name = variants.data[i].name,
                                    Info = variants.data[i].info == "None" ? "" : variants.data[i].info,
                                    Baseprice = "Base Price: " + variants.data[i].baseprice
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