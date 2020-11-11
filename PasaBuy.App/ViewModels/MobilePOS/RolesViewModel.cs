using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class RolesViewModel : BaseViewModel
    {

        private DelegateCommand _addTapped;

        public DelegateCommand AddTapped =>
          _addTapped ?? (_addTapped = new DelegateCommand(AddTappedFunction));

        private async void AddTappedFunction(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new PopupAddRole());
        }

        public static ObservableCollection<Models.POSFeature.RoleModel> _rolesList;
        public ObservableCollection<Models.POSFeature.RoleModel> RolesList
        {
            get
            {
                return _rolesList;
            }
            set
            {
                if (_rolesList != value)
                {
                    _rolesList = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
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
        public ICommand RefreshCommand { protected set; get; }
        public static string personnel_id = "0";

        public RolesViewModel()
        {
            this.RolesList = new ObservableCollection<Models.POSFeature.RoleModel>();
            LoadListRole();

            _rolesList = new ObservableCollection<Models.POSFeature.RoleModel>();

            AddRolePlusCommand = new Command<object>(AddRolePlusClicked);
            AddRoleCommand = new Command<object>(AddRoleClicked);
            DeleteRoleCommand = new Command<object>(DeleteRoleClicked);
            UpdateRoleCommand = new Command<object>(UpdateRoleClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                LoadListRole();
                IsRefreshing = false;
            });
        }

        public static void LoadRoleList()
        {
            try
            {
                _rolesList.Clear();
                Http.MobilePOS.Role.Instance.Listing("", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.RoleModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.RoleModel>(data);
                        if (role.data.Length > 0)
                        {
                            for (int i = 0; i < role.data.Length; i++)
                            {
                                _rolesList.Add(new Models.POSFeature.RoleModel()
                                {
                                    Id = role.data[i].ID,
                                    RoleTitle = role.data[i].title,
                                    RoleInfo = role.data[i].info,
                                    RoleStatus = role.data[i].status == "active" ? "Active" : "Inactive",
                                });
                            }
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L1RVM.", "OK");
            }
        }

        public void LoadListRole()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    this.RolesList.Clear();
                    Http.MobilePOS.Role.Instance.Listing("", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.RoleModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.RoleModel>(data);

                            for (int i = 0; i < role.data.Length; i++)
                            {
                                this.RolesList.Add(new Models.POSFeature.RoleModel()
                                {
                                    Id = role.data[i].ID,
                                    RoleTitle = role.data[i].title,
                                    RoleInfo = role.data[i].info,
                                    RoleStatus = role.data[i].status == "active" ? "Active" : "Inactive",
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L2RVM.", "OK");
                IsRunning = false;
            }
        }

        public Command<object> AddRolePlusCommand { get; set; }
        private async void AddRolePlusClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                await PopupNavigation.Instance.PushAsync(new PopupAddRole());
                IsRunning = false;
            }
        }
        public Command<object> AddRoleCommand { get; set; }

        private void AddRoleClicked(object obj)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    var role = obj as Models.POSFeature.RoleModel;
                    if (personnel_id == "0")
                    {
                        Http.MobilePOS.Personnel.Instance.Insert(PopupChooseRole.user_id, role.Id, "1234", async (bool success, string data) =>
                        {
                            if (success)
                            {
                                PersonnelsViewModel.RefreshPersonnel();
                                PopupNavigation.Instance.PopAsync();
                                await App.Current.MainPage.Navigation.PopModalAsync();
                                IsBusy = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsBusy = false;
                            }
                        });
                    }
                    else
                    {
                        Http.MobilePOS.Personnel.Instance.Update(personnel_id, role.Id, "1234", async (bool success, string data) =>
                        {
                            if (success)
                            {
                                personnel_id = "0";
                                PersonnelsViewModel.RefreshPersonnel();
                                await PopupNavigation.Instance.PopAsync();
                                IsBusy = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsBusy = false;
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-I1PVM.", "OK");
                IsBusy = false;
            }
        }

        public Command<object> DeleteRoleCommand { get; set; }

        private async void DeleteRoleClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var role = obj as Models.POSFeature.RoleModel;
                        Http.MobilePOS.Role.Instance.Delete(role.Id, (bool success, string data) =>
                        {
                            if (success)
                            {
                                LoadRoleList();
                                IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", Local.HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning = false;
                            }
                        });
                    }
                    else
                    {
                        IsRunning = false;
                    }
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-D1RVM.", "OK");
                IsBusy = false;
            }
        }

        public Command<object> UpdateRoleCommand { get; set; }

        private async void UpdateRoleClicked(object obj)    
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var role = obj as Models.POSFeature.RoleModel;
                AccessViewModel.role_id = role.Id;
                PopupEditRole.title = role.RoleTitle;
                PopupEditRole.description = role.RoleInfo;
                await PopupNavigation.Instance.PushAsync(new PopupEditRole());
                IsRunning = false;
            }
        }
    }
}
