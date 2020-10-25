using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class PaymentsViewModel : BaseViewModel
    {

        public ICommand SetupCommand
        {
            get
            {
                return new Command<string>((x) => SetupClicked(x));
            }
        }
        private async void SetupClicked(string id)
        {
            await PopupNavigation.Instance.PushAsync(new PopupSetupWallet());
        }

        public ICommand WithdrawCommand
        {
            get
            {
                return new Command<string>((x) => WithdrawClicked(x));
            }
        }
        private async void WithdrawClicked(string id)
        {
            await PopupNavigation.Instance.PushAsync(new PopupStoreWithdraw());
        }

        public PaymentsViewModel()
        {

        }
    }
}
