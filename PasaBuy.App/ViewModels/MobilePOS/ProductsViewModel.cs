using PasaBuy.App.Services;
using PasaBuy.App.ViewModels.MobilePOS.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class ProductsViewModel : ViewModelBase
    {
      
        private string _scannedText;

        public ProductsViewModel()
        {
 
        }


        public string ScannedText
        {
            get { return _scannedText; }
            set
            {
                _scannedText = value;
                RaisePropertyChanged(() => ScannedText);
            }
        }


        public ICommand AddProductCommand => new Command(async () => await AddProductCommandAsync());
        private async Task AddProductCommandAsync()
        {
            await NavigationService.NavigateToAsync<AddProductViewModel>(null);
        }

       

    }
}
