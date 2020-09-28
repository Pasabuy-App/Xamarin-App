using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
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
        public static string catid = "0";
        public PopupAddCategory()
        {
            InitializeComponent();
            if (catid != "0")
            {
                CatTitle.Text = "Edit Category";
                try
                {
                    TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, PSACache.Instance.UserInfo.stid, "2", "1", (bool success, string data) =>
                    {
                        if (success)
                        {
                            CategoryListData post = JsonConvert.DeserializeObject<CategoryListData>(data);
                            for (int i = 0; i < post.data.Length; i++)
                            {
                                string id = post.data[i].ID;
                                string title = post.data[i].title;
                                string info = post.data[i].info;
                                CatName.Text = title;
                                Description.Text = info;
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
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Description.Text) || !string.IsNullOrWhiteSpace(CatName.Text))
                {
                    if (catid == "0")
                    {
                        TindaPress.Category.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "product", CatName.Text, Description.Text, (bool success, string data) =>
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
                    else
                    {
                        TindaPress.Category.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, CatName.Text, Description.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                CategoryViewModel.categoriesList.Clear();
                                CategoryViewModel.LoadData();
                                CatTitle.Text = "Add Category";
                                PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}