using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAcceptOrder : PopupPage
    {
        public static string carItem = "car / sedan";
        public static string item_id = string.Empty;
        public static string storeName = "Pasabuy Store";
        public static string orderName = "Pasabuy Burger";
        public static string waypointAddress = "B10 L18 Narra St, Silcas Village, Brgy. San Francisco, Biñan, 4024 Laguna";
        public static string destinationAddress = "Southwoods Ave, Biñan, Laguna";
        public static string orderTime = "5mins ago";

        Stopwatch stopwatch = new Stopwatch();

        public PopupAcceptOrder()
        {
            InitializeComponent();

            Order.Text = item_id + " | "+orderTime;
            WaypointAddress.Text = waypointAddress;
            OriginAddress.Text = destinationAddress;
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
            HatidPress.Deliveries.Instance.Accept(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "","","","","","", "",(bool success, string data) => 
            {
                try
                {

                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                    Console.WriteLine("Handle not supported on device exception" + " " + fnsEx);
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                    Console.WriteLine("Handle not enabled on device exception" + " " + fneEx);
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    Console.WriteLine("Handle permission exception" + " " + pEx);
                }
                catch (Exception ex)
                {
                    // Unable to get location
                    Console.WriteLine("Unable to get location" + " " + ex);
                }
            });
           new Alert("Ok", "Add command and bind this to viewmodel", "ok");
        }
    }
}