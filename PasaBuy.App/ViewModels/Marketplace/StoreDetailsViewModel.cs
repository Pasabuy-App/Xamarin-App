using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class StoreDetailsViewModel : BaseViewModel
    {
        private string headerImagePath;
        public string HeaderImagePath
        {
            /*get { return App.BaseImageUrl + this.headerImagePath; }*/
            get { return "https://images.summitmedia-digital.com/spotph/images/articles/2016/BurgerMcdo2.jpg"; }
            set { this.headerImagePath = value; }
        }

        private void FavouriteButtonClicked(object obj)
        {
            var button = obj as SfButton;

            if (button == null)
            {
                return;
            }

            if (button.Text == "\ue701")
            {
                button.Text = "\ue732";
                Application.Current.Resources.TryGetValue("PrimaryColor", out var retVal);
                button.TextColor = (Color)retVal;
            }
            else
            {
                button.Text = "\ue701";
                Application.Current.Resources.TryGetValue("Gray-600", out var retVal);
                button.TextColor = (Color)retVal;
            }
        }
    }

}
