using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;
using MobilePOS;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using System.Globalization;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewOperations : PopupPage
    {
        public static string opid;
        public PopupViewOperations()
        {
            InitializeComponent();
            try
            {
                Operation.Instance.ListByID_Sales(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, opid, (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Operations datas = JsonConvert.DeserializeObject<Operations>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string open_time = string.IsNullOrEmpty(datas.data[i].date_open) ? "0000-00-00 00:00:00" : datas.data[i].date_open;
                            string close_time = string.IsNullOrEmpty(datas.data[i].date_close) ? "0000-00-00 00:00:00" : datas.data[i].date_close;
                            string dates = string.IsNullOrEmpty(datas.data[i].date) ? "0000-00-00 00:00:00" : datas.data[i].date;
                            DateTime open = DateTime.ParseExact(open_time, "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime close = DateTime.ParseExact(close_time, "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime date = DateTime.ParseExact(dates, "yyyy-MM-dd HH:mm:ss", provider);

                            Opened.Text = open.ToString("hh:mm tt");
                            Closed.Text = close.ToString("hh:mm tt");
                            Date.Text = date.ToString("MMMM dd, yyyy");
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

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}