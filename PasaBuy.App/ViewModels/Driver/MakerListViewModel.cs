using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Driver
{
    public class MakerListViewModel
    {
        public ObservableCollection<TypeListModel> _MakerList;

        public ObservableCollection<TypeListModel> MakerList
        {
            get
            {
                return _MakerList;
            }
            set
            {
                _MakerList = value;
            }
        }
        public MakerListViewModel(string val)
        {
            try
            {
                _MakerList = new ObservableCollection<TypeListModel>();
                Http.HatidPress.Vehicle.Instance.Listing_Maker(val, "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        TypeListModel type = JsonConvert.DeserializeObject<TypeListModel>(data);
                        for (int i = 0; i < type.data.Length; i++)
                        {
                            _MakerList.Add(new TypeListModel()
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
                    new Controllers.Notice.Alert("Error Code: HPV2VHE-MV1MLVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2VHE-MV1MLVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2VHE-MV1MLVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2VHE-MV1MLVM-" + err.ToString());
                }
            }
        }
    }
}
