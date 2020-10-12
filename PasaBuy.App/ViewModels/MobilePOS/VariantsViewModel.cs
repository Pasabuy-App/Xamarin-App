using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews.Management;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
            await Application.Current.MainPage.Navigation.PushModalAsync(new OptionsView());
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
            this.VariantsList = new ObservableCollection<Variants>()
            {
                new Variants
                {
                   Name = "Size",
                   Id = "23"

                },
                new Variants
                {
                   Name = "Flavor",
                   Id = "42"

                },

            };

            this.OptionsList = new ObservableCollection<Options>()
            {
                new Options
                {
                   Name = "Option 1",
                   Id = "123"

                },
                new Options
                {
                   Name = "Option 2",
                   Id = "2"

                },
                new Options
                {
                   Name = "Option 3",
                   Id = "15"

                },

            };
        }
    }
}
