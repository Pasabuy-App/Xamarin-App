using PasaBuy.App.Commands;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class VariantsViewModel : BaseViewModel
    {
        public static ObservableCollection<Variants> variantsList;

        private DelegateCommand _addVariantsCommand;

        public DelegateCommand AddVariantsCommand =>
          _addVariantsCommand ?? (_addVariantsCommand = new DelegateCommand(AddVariantsClicked));

        private async void AddVariantsClicked(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddVariants());
        }

        public ObservableCollection<Variants> VariantsList
        {
            get 
            { 
                return variantsList; 
            }
            set 
            {
                variantsList = value;
                this.NotifyPropertyChanged();
            }
        }

        public VariantsViewModel()
        {
            variantsList = new ObservableCollection<Variants>();
            for (int i = 0; i < 2; i++)
            {
                variantsList.Add(new Variants()
                {
                    Name = "Size",
                });
            }
        }
    }
}
