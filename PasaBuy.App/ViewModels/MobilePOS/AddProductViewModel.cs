using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Services;

using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AddProductViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<CategoryData> categoriesList;
        public ObservableCollection<CategoryData> CategoryList
        {
            get { return categoriesList; }
            set { categoriesList = value; this.NotifyPropertyChanged(); }
        }
        #endregion
        public AddProductViewModel()
        {
            categoriesList = new ObservableCollection<CategoryData>();
            categoriesList.Clear();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", PSACache.Instance.UserInfo.user_type, "2", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        CategoryListData post = JsonConvert.DeserializeObject<CategoryListData>(data);
                        for (int i = 0; i < post.data.Length; i++)
                        {
                            string id = post.data[i].ID;
                            string title = post.data[i].title;
                            string info = post.data[i].info;
                            categoriesList.Add(new CategoryData()
                            {
                                Title = title,
                                ID = id
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
            }
        }
    }
}
