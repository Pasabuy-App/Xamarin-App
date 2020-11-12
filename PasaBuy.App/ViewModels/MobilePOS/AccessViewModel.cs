using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class AccessViewModel : BaseViewModel
    {
        private ObservableCollection<Models.POSFeature.AccessGroup> _accessGroup;
        public static ObservableCollection<Models.POSFeature.AccessModel> _accessList;
        public static ObservableCollection<Models.POSFeature.AccessModel> _myAccessList;
        public static ObservableCollection<Models.POSFeature.AccessModel> _AccessList;

        public ObservableCollection<Models.POSFeature.AccessModel> _MyAccessList
        {
            get
            {
                return _AccessList;
            }
            set
            {
                _AccessList = value;
                this.NotifyPropertyChanged();
            }
        }
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
        public static string role_id = "0";
        public AccessViewModel()
        {
            _accessGroup = new ObservableCollection<Models.POSFeature.AccessGroup>();
            _myAccessList = new ObservableCollection<Models.POSFeature.AccessModel>();
            _AccessList = new ObservableCollection<Models.POSFeature.AccessModel>();
            if (role_id == "0")
            {
                LoadAccess();
            }
            else
            {
                LoadRoleList();
            }
            this.InsertRoleCommand = new DelegateCommand(InsertRoleClicked);
            this.UpdateRoleCommand = new DelegateCommand(UpdateRoleClicked);
        }
        public void LoadRoleList()
        {

            try
            {
                Http.MobilePOS.Role.Instance.Listing(role_id, "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.RoleModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.RoleModel>(data);

                        for (int i = 0; i < role.data[0].permission.Count; i++)
                        {
                            bool status = role.data[0].permission[i].status == "active" ? true : false;
                            _AccessList.Add(new Models.POSFeature.AccessModel()
                            {
                                ID = role.data[0].permission[i].access,
                                Status = status
                            });
                        }
                        LoadAccess2();
                    }
                    else
                    {
                        new Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L1AVM.", "OK");
            }
        }
        public void LoadAccess2()
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
                                _accessList = new ObservableCollection<Models.POSFeature.AccessModel>();
                                for (int j = 0; j < access.data[i].access.Count; j++)
                                {
                                    bool status = false;
                                    if (_AccessList.Any(p => p.ID == access.data[i].access[j].ID))
                                    {
                                        status = true;
                                    }
                                    /*foreach (Models.POSFeature.AccessModel var in _myAccessList)
                                    {
                                        if (var.ID == access.data[i].access[j].ID)
                                        {
                                            status = true;
                                        }
                                    }*/
                                    _accessList.Add(new Models.POSFeature.AccessModel()
                                    {
                                        ID = access.data[i].access[j].ID,
                                        AccessName = access.data[i].access[j].AccessName,
                                        Status = status
                                    });
                                }
                                _accessGroup.Add(new Models.POSFeature.AccessGroup()
                                {
                                    GroupName = access.data[i].name,
                                    AccessList = _accessList
                                });

                                for (int j = 0; j < AccessList.Count; j++)
                                {
                                    _myAccessList.Add(new Models.POSFeature.AccessModel()
                                    {
                                        ID = AccessList[j].ID,
                                        AccessName = AccessList[j].AccessName,
                                        Status = AccessList[j].Status
                                    });
                                }
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
        private void UpdateRoleClicked(object obj)
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
                        Http.MobilePOS.Role.Instance.Update(role_id, this.Name, this.Description, _myAccessList, (bool success, string data) =>
                        {
                            if (success)
                            {
                                role_id = "0";
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-U1AVM.", "OK");
                IsRunning = false;
            }

            /*foreach (Models.POSFeature.AccessModel var in _myAccessList)
            {
                Debug.WriteLine("var.Status: " + var.Status + ". ." + var.AccessName );
            }*/
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

            /*foreach (Models.POSFeature.AccessModel var in _myAccessList)
            {
                Console.WriteLine("var.Status: ." + var.Status + ". ." + var.AccessId);
            }*/
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
                                for (int j = 0; j < access.data[i].access.Count; j++)
                                {
                                    _myAccessList.Add(new Models.POSFeature.AccessModel()
                                    {
                                        ID = access.data[i].access[j].ID,
                                        Status = false
                                    });
                                }
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
            foreach (Models.POSFeature.AccessModel var in _myAccessList)
            {
                if (var.ID == access_id)
                {
                    var.Status = true;
                    break;
                }
            }
            /*_myAccessList.Add(new Models.POSFeature.AccessModel()
            {
                ID = access_id
            });*/
        }

        public static void Remove(string access_id)
        {
            foreach (Models.POSFeature.AccessModel var in _myAccessList)
            {
                if (var.ID == access_id)
                {
                    var.Status = false;
                    //_myAccessList.Remove(var);
                    break;
                }
            }
        }

        public DelegateCommand InsertRoleCommand { get; set; }
        public DelegateCommand UpdateRoleCommand { get; set; }
    }
}
