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
            new Alert("Demoguy Notice", "Saving of user profile data is not yet implemented. Thank you for your patience!", "AGREE");
        }
    }
}