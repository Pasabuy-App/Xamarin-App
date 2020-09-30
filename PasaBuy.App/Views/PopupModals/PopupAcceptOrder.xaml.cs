using PasaBuy.App.Controllers.Notice;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAcceptOrder : PopupPage
    {
        Stopwatch stopwatch = new Stopwatch();
        
        public PopupAcceptOrder()
        {
            InitializeComponent();
            OrderTime.Text = "30";
            OrderTimer(true);
        }

        public void OrderTimer(Boolean flag)
        {
            int TimeLimit = 30;
            stopwatch.Start();
            if (flag == true)
            {
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {

                    OrderTime.Text = (TimeLimit - stopwatch.Elapsed.Seconds).ToString();
                    if (30 - stopwatch.Elapsed.Seconds == 1)
                    {
                        PopupNavigation.Instance.PopAsync();
                        return false;
                    }
                    return true;
                });

            }
            else
            {
                stopwatch.Stop();
            }
           
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
        }

        private void AcceptOrder(object sender, EventArgs e)
        {
            new Alert("Ok", "Add command and bind this to viewmodel", "ok");
        }
    }
}