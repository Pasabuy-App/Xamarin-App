using PasaBuy.App.Models.Marketplace;
using Syncfusion.XForms.Buttons;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private ObservableCollection<Variants> _variantsList;
        private ObservableCollection<Options> _optionsList;

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
     
            ObservableCollection<Options> size_options = new ObservableCollection<Options>();
            size_options.Add(new Options() { Name = "Medium", Price = "+25.00" });
            size_options.Add(new Options() { Name = "Large", Price = "+45.00" });
            size_options.Add(new Options() { Name = "Grande", Price = "+65.00" });
            ObservableCollection<Options> sweetness_options = new ObservableCollection<Options>();
            sweetness_options.Add(new Options() { Name = "25%", Price = "+0.00" });
            sweetness_options.Add(new Options() { Name = "50%", Price = "+0.00" });
            sweetness_options.Add(new Options() { Name = "75%", Price = "+0.00" });
            sweetness_options.Add(new Options() { Name = "100%", Price = "+0.00" });

            _variantsList.Add(new Variants() { Name = "Size", options = size_options, GroupKey = "Size" });
            _variantsList.Add(new Variants() { Name = "Sweetness Level", options = sweetness_options , GroupKey = "Sweetness Level"});

            //this.VariantsList = new ObservableCollection<Variants>()
            //{
            //    new Variants()
            //    {
            //        Name = "Size",
            //        options = size_options
            //    },
            //    new Variants()
            //    {
            //        Name = "Sweetness Level",
            //        options = sweetness_options
            //    },

            //};



        }
    }
}
