using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class VariantsViewModel : BaseViewModel
    {
        public static ObservableCollection<Variants> variantsList;

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
