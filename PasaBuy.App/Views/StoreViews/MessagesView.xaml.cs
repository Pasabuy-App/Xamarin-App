using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.ViewModels.Chat;
using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TindaPress;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagesView : ContentPage
    {
        public MessagesView()
        {
            InitializeComponent();
            this.BindingContext = new StoreMessageViewModel();
        }
    }
}