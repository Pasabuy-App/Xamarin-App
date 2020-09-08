using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.Controllers.Notice
{
    public class ConfirmAlert
    {
        #region Constuctor
        public ConfirmAlert(string title, string message, string confirm, string cancel)
        {
            ShowMessage(title, message, confirm, cancel);
        }
        #endregion

        public async void ShowMessage(string title, string message, string confirm, string cancel)
        {
            if (App.Current.MainPage != null)
            {
                await App.Current.MainPage.DisplayAlert(title, message, confirm, cancel); ;
            }
        }
    }


}
