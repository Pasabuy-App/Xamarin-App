using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Marketplace;
using Syncfusion.XForms.Buttons;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private ObservableCollection<Variants> _variantsList;
        private ObservableCollection<Options> _optionsList;
        private bool isChecked = false;

        public SfRadioGroupKey GroupKey { get; set; }

        public ICommand AddToCartCommand
        {
            get
            {
                return new Command<string>((x) => AddToCartClicked(x));
            }
        }


        private async void AddToCartClicked(string id)
        {
            new Alert("Something went Wrong", "test", "OK");
            
        }


        public bool IsChecked
        {
            get 
            { 
                return isChecked; 
            }
            set 
            {
                isChecked = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Options> OptionsList
        {
            get
            {
                GroupKey = new SfRadioGroupKey();

                if (_optionsList != null)
                {
                    foreach (var item in _optionsList)
                    {
                        item.GroupKey = GroupKey;
                    }
                }
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
            ObservableCollection<Options> flavor = new ObservableCollection<Options>();
            flavor.Add(new Options() { Name = "Spicy", Price = "+0.00" });
            flavor.Add(new Options() { Name = "Original", Price = "+0.00" });


            _variantsList.Add(new Variants() { Name = "Size", options = size_options });
            _variantsList.Add(new Variants() { Name = "Sweetness Level", options = sweetness_options});
            _variantsList.Add(new Variants() { Name = "Flavor", options = flavor });


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
