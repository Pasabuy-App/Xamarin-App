using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.MobilePOS;
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
    public partial class PopupAddCategory : PopupPage
    {
        public PopupAddCategory()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Description.Text) || !string.IsNullOrWhiteSpace(CatName.Text))
            {
                try
                {
                    TindaPress.Category.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.user_type, "product", CatName.Text, Description.Text, (bool success, string data) =>
                    {
                        if (success)
                        {
                            CategoryViewModel.categoriesList.Clear();
                            CategoryViewModel.LoadData();
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20430.", "OK");
                }
            }
        }
    }
}