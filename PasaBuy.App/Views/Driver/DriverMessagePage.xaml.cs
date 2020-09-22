using FFImageLoading;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.ViewModels.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverMessagePage : ContentPage
    {
        public static bool isFirstID = false;
        public static int ids = 0;
        public static int LastIndex = 11;
        public DriverMessagePage()
        {
            InitializeComponent();
            LastIndex = 11;
            isFirstID = false;
            ids = 0;
        }

        private void ListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as ChatDetail;
            if (DriverRecentChatViewModel.chatItems.Last() == item && DriverRecentChatViewModel.chatItems.Count() != 1)
            {
                if (DriverRecentChatViewModel.chatItems.IndexOf(item) >= LastIndex)
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
                        DriverRecentChatViewModel.LoadMesssage(ids.ToString());
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