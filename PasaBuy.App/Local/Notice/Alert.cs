using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.Controllers.Notice
{
    public class Alert
    {
        #region Constuctor
        public Alert(string title, string message, string confirm)
        {
            ShowMessage(title, message, confirm);
        }
        #endregion

        public async void ShowMessage(string title, string message, string confirm)
        {
            if (App.Current.MainPage != null)
            {
                await App.Current.MainPage.DisplayAlert(title, message, confirm);
            }
        }
    }
}
