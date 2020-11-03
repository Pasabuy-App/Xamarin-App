using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AccessViewModel : BaseViewModel
    {
        private ObservableCollection<Models.POSFeature.AccessGroup> _accessGroup;
        public static ObservableCollection<Models.POSFeature.AccessModel> _accessList;
        public static ObservableCollection<Models.POSFeature.AccessModel> _myAccessList;

        public ObservableCollection<Models.POSFeature.AccessGroup> AccessGroup
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

        public ObservableCollection<Models.POSFeature.AccessModel> AccessList
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
        public ObservableCollection<Models.POSFeature.AccessModel> MyAccessList
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
        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public AccessViewModel()
        {
            _accessGroup = new ObservableCollection<Models.POSFeature.AccessGroup>();
            _myAccessList = new ObservableCollection<Models.POSFeature.AccessModel>();
            LoadAccess();
            this.InsertRoleCommand = new DelegateCommand(InsertRoleClicked);
        }
        private void InsertRoleClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    if (string.IsNullOrEmpty(this.Description) || string.IsNullOrEmpty(this.Name) || _myAccessList.Count == 0)
                    {
                        new Alert("Notice to User", "Please input title, description or add an access.", "Try Again");
                        IsRunning = false;
                    }
                    else
                    {
                        Http.MobilePOS.Role.Instance.Insert(this.Name, this.Description, _myAccessList, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RolesViewModel.LoadRoleList();
                                PopupNavigation.Instance.PopAsync();
                                IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-I1AVM.", "OK");
                IsRunning = false;
            }
        }
        public void LoadAccess()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Role.Instance.AccessList((bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.AccessGroup access = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.AccessGroup>(data);
                            for (int i = 0; i < access.data.Length; i++)
                            {
                                _accessGroup.Add(new Models.POSFeature.AccessGroup()
                                {
                                    GroupName = access.data[i].name,
                                    AccessList = access.data[i].access
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2RAL-L1AVM.", "OK");
                IsRunning = false;
            }
        }

        public static void Insert(string access_id)
        {
            _myAccessList.Add(new Models.POSFeature.AccessModel()
            {
                ID = access_id
            });
        }

        public static void Remove(string access_id)
        {
            foreach (Models.POSFeature.AccessModel var in _myAccessList)
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
