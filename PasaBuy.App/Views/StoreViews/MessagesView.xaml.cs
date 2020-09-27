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
        public static bool isFirstID = false;
        public static int ids = 0;
        public static int LastIndex = 11;
        public MessagesView()
        {
            InitializeComponent();
            this.BindingContext = new StoreMessageViewModel();
            LastIndex = 11;
            isFirstID = false;
            ids = 0;
        }

        private void ListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as ChatDetail;
            if (StoreMessageViewModel.storeChatList.Last() == item && StoreMessageViewModel.storeChatList.Count() != 1)
            {
                if (StoreMessageViewModel.storeChatList.IndexOf(item) >= LastIndex)
                {
                    try
                    {
                        if (isFirstID)
                        {
                            ids += 7;
                        }
                        else
                        {
                            isFirstID = true;
                        }
                        LastIndex += 6;
                        StoreMessageViewModel.LoadMesssage(ids.ToString());
                    }
                    catch
                    {
                        new Alert("Something went Wrong", "Please contact administrator. Error Code: 20435.", "OK");
                    }
                }
            }
        }
    }
}