using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.Driver
{
    public class DocTypeListViewModel
    {
        public ObservableCollection<TypeListModel> _DocTypeList;

        public ObservableCollection<TypeListModel> DocTypeList
        {
            get
            {
                return _DocTypeList;
            }
            set
            {
                _DocTypeList = value;
            }
        }
        public DocTypeListViewModel()
        {
            try
            {
                _DocTypeList = new ObservableCollection<TypeListModel>();
                Http.HatidPress.Documents.Instance.Listing_DocType((bool success, string data) =>
                {
                    if (success)
                    {
                        TypeListModel type = JsonConvert.DeserializeObject<TypeListModel>(data);
                        for (int i = 0; i < type.data.Length; i++)
                        {
                            _DocTypeList.Add(new TypeListModel()
                            {
                                ID = type.data[i].ID,
                                Title = type.data[i].title
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
                    new Controllers.Notice.Alert("Error Code: HPV2DOC-LT1DTLVMV", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2DOC-LT1DTLVMV-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-LT1DTLVMV.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2DOC-LT1DTLVMV-" + err.ToString());
                }
            }
        }
    }
}
