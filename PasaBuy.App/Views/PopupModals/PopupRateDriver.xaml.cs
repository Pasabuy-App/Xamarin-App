using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupRateDriver : PopupPage
    {
        public static string order_id = string.Empty;
        public static string avatar = string.Empty;
        public static string mover_name = string.Empty;
        public static string mover_id = string.Empty;

        public PopupRateDriver()
        {
            InitializeComponent();
            OrderID.Text = order_id;
            Avatar.Source = avatar;
            MoverName.Text = mover_name;
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void SubmitButton(object sender, EventArgs e)
        {
            try
            {
                Http.HatidPress.MoverData.Instance.Rating(mover_id, Rating.Value.ToString(), Remarks.Text, async (bool success, string data) =>
                {
                    if (success)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2MVR-R1PURD.", "OK");
            }
        }
    }
}