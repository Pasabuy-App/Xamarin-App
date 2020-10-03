using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private ObservableCollection<Variants> _variantsList;

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

        public ProductDetailViewModel()
        {
            _variantsList = new ObservableCollection<Variants>();

            for (int i = 0; i < 3; i++)
            {
                _variantsList.Add(new Variants
                {
                    Name = "Size"
                });
            }
           
        }
    }
}
