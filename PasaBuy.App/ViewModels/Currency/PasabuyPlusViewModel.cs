using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Currency;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public static string currency_id;
        public static string wallet_id;
        private DelegateCommand _TopUpCommand;
        public DelegateCommand TopUpCommand =>
         _TopUpCommand ?? (_TopUpCommand = new DelegateCommand(TopUpClicked));

        public static ObservableCollection<PasabuyPlusModel> _PasabuyPlusList;
        public ObservableCollection<PasabuyPlusModel> PasabuyPlusList
        {
            get
            {
                return _PasabuyPlusList;
            }
            set
            {
                _PasabuyPlusList = value;
                this.NotifyPropertyChanged();
            }
        }
        private async void TopUpClicked(object obj)
        {
            Views.PopupModals.PopupShowWalletCredit.wallet_id = wallet_id;
            Views.PopupModals.PopupShowWalletCredit.wallet_title = "Pasabuy Plus ID";

            await PopupNavigation.Instance.PushAsync(new Views.PopupModals.PopupShowWalletCredit());
        }

        public PasabuyPlusViewModel()
        {
            //this.Amount = "12.25"; TopUpCommand
            _PasabuyPlusList = new ObservableCollection<PasabuyPlusModel>();
            CreateWallet();
        }

        public void LoadData(string currency, string offset)
        {
            try
            {
                CoinPress.Wallet.Instance.Transactions(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", currency, offset, (bool success, string data) =>
                {
                    if (success)
                    {
                        PasabuyPlusModel plus = JsonConvert.DeserializeObject<PasabuyPlusModel>(data);
                        if (plus.data.Length > 0)
                        {
                            for (int i = 0; i < plus.data.Length; i++)
                            {
                                _PasabuyPlusList.Add(new PasabuyPlusModel()
                                {
                                    ProfileImage = PSAProc.GetUrl(plus.data[i].avatar),
                                    Name = plus.data[i].name,
                                    Note = string.IsNullOrEmpty(plus.data[i].remarks) || plus.data[i].remarks == "None" ? "" : plus.data[i].remarks,
                                    Amount = Convert.ToDouble(plus.data[i].amount),
                                    Date = new DateTime(Convert.ToInt32(plus.data[i].date_created.Substring(0, 4)), Convert.ToInt32(plus.data[i].date_created.Substring(5, 2)), Convert.ToInt32(plus.data[i].date_created.Substring(8, 2))),
                                    IsCredited = plus.data[i].type == "sender" ? false : true
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

        public void CreateWallet()
        {
            try
            {
                CoinPress.Wallet.Instance.Create(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "PLS", async (bool success, string data) =>
                {
                    if (success)
                    {
                        PasabuyPlusModel plus = JsonConvert.DeserializeObject<PasabuyPlusModel>(data);
                        for (int i = 0; i < plus.data.Length; i++)
                        {
                            wallet_id = plus.data[i].public_key;
                            LoadBalance();
                            await Task.Delay(500);
                            LoadData(plus.data[i].currency_id, "");
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

        public void LoadBalance()
        {
            try
            {
                CoinPress.Wallet.Instance.Balance(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "PLS", (bool success, string data) =>
                {
                    if (success)
                    {
                        PasabuyPlusModel plus = JsonConvert.DeserializeObject<PasabuyPlusModel>(data);
                        this.Amount = plus.balance;
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
