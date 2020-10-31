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
    public class ModelListViewModel : BaseViewModel
    {
        public static ObservableCollection<TypeListModel> _ModelList;

        public ObservableCollection<TypeListModel> ModelList
        {
            get
            {
                return _ModelList;
            }
            set
            {
                _ModelList = value;
                this.NotifyPropertyChanged();
            }
        }
        public ModelListViewModel(string val)
        {
            try
            {
                _ModelList = new ObservableCollection<TypeListModel>();
                Http.HatidPress.Vehicle.Instance.Listing_Model(val, "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        TypeListModel type = JsonConvert.DeserializeObject<TypeListModel>(data);
                        for (int i = 0; i < type.data.Length; i++)
                        {
                            _ModelList.Add(new TypeListModel()
                            {
                                ID = type.data[i].ID,
                                Title = type.data[i].title
                            });
                        }
                        Console.WriteLine("data: " + data);
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
        public static void ClearList()
        {
            _ModelList.Clear();
        }
    }
}
