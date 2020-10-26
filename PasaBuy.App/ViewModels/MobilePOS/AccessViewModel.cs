using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AccessViewModel : BaseViewModel
    {
        private ObservableCollection<AccessGroup> _accessGroup;
        public static ObservableCollection<AccessModel> _accessList;
        public static ObservableCollection<AccessModel> _myAccessList;

        public ObservableCollection<AccessGroup> AccessGroup
        {
            get
            {
                return _accessGroup;
            }
            set
            {
                _accessGroup = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<AccessModel> AccessList
        {
            get
            {
                return _accessList;
            }
            set
            {
                _accessList = value;
                this.NotifyPropertyChanged();
            }
        }
        public ObservableCollection<AccessModel> MyAccessList
        {
            get
            {
                return _myAccessList;
            }
            set
            {
                _myAccessList = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChanged();
            }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                this.NotifyPropertyChanged();
            }
        }

        public AccessViewModel()
        {
            _accessGroup = new ObservableCollection<AccessGroup>();
            _myAccessList = new ObservableCollection<AccessModel>();
            LoadAccess();
            this.InsertRoleCommand = new DelegateCommand(InsertRoleClicked);
        }
        private void InsertRoleClicked(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(this.Description) || string.IsNullOrEmpty(this.Name) || _myAccessList.Count == 0)
                {
                    new Alert("Notice to User", "Please input title, description or add an access.", "Try Again");
                }
                else
                {
                    Http.POSFeature.Instance.Role_Insert(this.Name, this.Description, _myAccessList, (bool success, string data) =>
                    {
                        if (success)
                        {
                            PopupNavigation.Instance.PopAsync();
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        public void LoadAccess()
        {
            try
            {
                Http.POSFeature.Instance.Access_List((bool success, string data) =>
                {
                    if (success)
                    {
                        AccessGroup access = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessGroup>(data);
                        for (int i = 0; i < access.data.Length; i++)
                        {
                            _accessGroup.Add(new AccessGroup() { GroupName = access.data[i].name, AccessList = access.data[i].access });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public static void Insert(string access_id)
        {
            _myAccessList.Add(new AccessModel()
            {
                ID = access_id
            });
        }

        public static void Remove(string access_id)
        {
            foreach (AccessModel var in _myAccessList)
            {
                if (var.ID == access_id)
                {
                    _myAccessList.Remove(var);
                    break;
                }
            }
        }

        public DelegateCommand InsertRoleCommand { get; set; }
    }
}
