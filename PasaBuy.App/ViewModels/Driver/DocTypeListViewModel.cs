using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                Http.HatidFeature.Instance.Listing_DocTyoe((bool success, string data) =>
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
}
