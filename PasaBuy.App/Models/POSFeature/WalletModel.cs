using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.Models.POSFeature
{
    public class WalletModel
    {
        public WalletData[] data;
    }
    public class WalletData
    {
        public string pubkey = string.Empty;
        public string assigned_by = string.Empty;
        public string balance = string.Empty;
        public ObservableCollection<StorePayment> transactions;
    }
}
