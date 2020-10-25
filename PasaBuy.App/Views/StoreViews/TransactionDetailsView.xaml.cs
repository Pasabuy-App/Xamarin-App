using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
using PasaBuy.App.Views.Chat;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDetailsView : ContentPage
    {
        public static string id = string.Empty;
        public static string customer = string.Empty;
        public static string stid = string.Empty;
        public static string user_id = string.Empty;
        public static string avatar = string.Empty;
        public static string orderid = string.Empty;
        public static string totalprice = string.Empty;
        public static string method = string.Empty;
        public static string datecreated = string.Empty;
        public static string order_type = string.Empty;
        public static string stage_type = string.Empty;
        public bool isClicked = false;
        public TransactionDetailsView()
        {
            InitializeComponent();
            this.BindingContext = new OrderDetailsViewModel();
            Customer.Text = customer;
            OrderID.Text = orderid;
            DateCreated.Text = datecreated;
            TotalPrice.Text = totalprice;
            Method.Text = method;
            if (order_type == "Completed" || order_type == "Received")
            {
                MessageButton.IsVisible = true;
            }
            else
            {
                MessageButton.IsVisible = false;
            }
            if (order_type == "Completed" || order_type == "Declined")
            {
                Decline_Accept.IsVisible = false;

            }
            else
            {
                Decline_Accept.IsVisible = true;
                if (order_type == "Received")
                {
                    Accepted.Text = "Complete";
                    Declined.IsVisible = false;
                }
                else
                {
                    Accepted.Text = "Accept";
                    Declined.IsVisible = true;
                }
            }
            isClicked = false;
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Declined_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!isClicked)
                {
                    isClicked = true;
                    //new Alert("ClassID: " + id, "Yes", "OK"); 
                    OrderDetailsViewModel.ProcessOrder(id, "cancelled", stage_type);
                    await Navigation.PopModalAsync();
                    await Task.Delay(200);
                    isClicked = false;
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private async void Accepted_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!isClicked)
                {
                    isClicked = true;
                    //new Alert("ClassID: " + id, "Yes", "OK"); 
                    if (order_type == "Received")
                    {
                        OrderDetailsViewModel.ProcessOrder(id, "shipping", stage_type);
                    }
                    else
                    {
                        OrderDetailsViewModel.ProcessOrder(id, "received", stage_type);
                    }
                    await Navigation.PopModalAsync();
                    await Task.Delay(200);
                    isClicked = false;
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        private void MessageCustomer(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ViewModels.Chat.StoreConversationViewModel.refresh = 0;
                await Task.Delay(200);

                ViewModels.Chat.StoreConversationViewModel.ProfileNames = customer;
                ViewModels.Chat.StoreConversationViewModel.ProfileImages = avatar;
                ViewModels.Chat.StoreConversationViewModel.user_id = user_id;
                ViewModels.Chat.StoreConversationViewModel.storeid = PSACache.Instance.UserInfo.stid;
                await ((App.Current.MainPage as MasterDetailPage).Detail as NavigationPage).Navigation.PushModalAsync(new StoreConversationPage());

                //await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ChatMessagePage()));
            });
        }
    }
}