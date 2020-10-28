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
    public class TypeListViewModel
    {
        public ObservableCollection<TypeListModel> _TypeList;

        public ObservableCollection<TypeListModel> TypeList
        {
            get
            {
                return _TypeList;
            }
            set
            {
                _TypeList = value;
            }
        }
        public TypeListViewModel()
        {
            try
            {
                _TypeList = new ObservableCollection<TypeListModel>();
                Http.HatidFeature.Instance.Listing_Type("", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        TypeListModel type = JsonConvert.DeserializeObject<TypeListModel>(data);
                        for (int i = 0; i < type.data.Length; i++)
                        {
                            _TypeList.Add(new TypeListModel()
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
