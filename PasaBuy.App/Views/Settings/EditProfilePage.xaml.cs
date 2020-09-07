using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DataVice;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
            InitializeComponent();

            Fname.Text = PSACache.Instance.UserInfo.fname;
            Lname.Text = PSACache.Instance.UserInfo.lname;
            Nname.Text = PSACache.Instance.UserInfo.dname;
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// Invoked when save button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //new Alert("Demoguy Notice", "Saving of user profile data is not yet implemented. Thank you for your patience!", "AGREE");
            Users.Instance.EditProfile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, Fname.Text, Lname.Text, Nname.Text, (bool success, string data) =>
            {
                if (success)
                {
                    PSACache.Instance.UserInfo.dname = Nname.Text;
                    PSACache.Instance.UserInfo.lname = Lname.Text;
                    PSACache.Instance.UserInfo.fname = Fname.Text;
                    Navigation.PopModalAsync();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }
    }
}