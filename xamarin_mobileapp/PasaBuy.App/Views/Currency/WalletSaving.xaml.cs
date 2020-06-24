using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    /// <summary>
    /// My wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletSaving : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletSaving"/> class.
        /// </summary>
        public WalletSaving()
        {
            InitializeComponent();
        }
        
    }
}