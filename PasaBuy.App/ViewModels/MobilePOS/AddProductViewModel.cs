using Newtonsoft.Json;
using PasaBuy.App.Local;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AddProductViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<Models.TindaFeature.CategoryModel> categoriesList;
        public ObservableCollection<Models.TindaFeature.CategoryModel> CategoryList
        {
            get 
            { 
                return categoriesList; 
            }
            set 
            {
                if (categoriesList != value)
                {
                    categoriesList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        #endregion
        public AddProductViewModel()
        {
            categoriesList = new ObservableCollection<Models.TindaFeature.CategoryModel>();
            categoriesList.Clear();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                Http.TindaPress.Category.Instance.Listing("", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.CategoryModel category = JsonConvert.DeserializeObject<Models.TindaFeature.CategoryModel>(data);
                        for (int i = 0; i < category.data.Length; i++)
                        {
                            categoriesList.Add(new Models.TindaFeature.CategoryModel()
                            {
                                Title = category.data[i].title,
                                ID = category.data[i].ID
                            });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-L1APVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-L1APVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L1APVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-L1APVM-" + err.ToString());
                }
            }
        }
    }
}
