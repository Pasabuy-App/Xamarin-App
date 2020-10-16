using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.ViewModels.Currency
{
    public class PasabuyPlusViewModel : BaseViewModel
    {

        public string _Amount;
        
        public string Amount
        {
            get
            {
                return this._Amount;
            }

            set
            {
                this._Amount = value;
                this.NotifyPropertyChanged();
            }
        }
        public PasabuyPlusViewModel()
        {
            this.Amount = "12.25";
        }
    }
}
