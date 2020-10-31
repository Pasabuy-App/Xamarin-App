using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public static ObservableCollection<RolesModel> _rolesList;
        public ObservableCollection<RolesModel> RolesList
        {
            get
            {
                return _rolesList;
            }
            set
            {
                _rolesList = value;
                this.NotifyPropertyChanged();
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
        public ICommand RefreshCommand { protected set; get; }

        public RolesViewModel()
        {
            _rolesList = new ObservableCollection<RolesModel>();
            LoadRoleList();

            AddRoleCommand = new Command<object>(AddRoleClicked);
            DeleteRoleCommand = new Command<object>(DeleteRoleClicked);
            UpdateRoleCommand = new Command<object>(UpdateRoleClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                _rolesList.Clear();
                LoadRoleList();
                IsRefreshing = false;
            });
        }

        public static void LoadRoleList()
        {
            try
            {
                Http.POSFeature.Instance.Role_List("", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        RolesModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<RolesModel>(data);
                        if (role.data.Length > 0)
                        {
                            for (int i = 0; i < role.data.Length; i++)
                            {
                                _rolesList.Add(new RolesModel()
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public void LoadListRole()
        {
            try
            {
                this.RolesList.Clear();
                Http.POSFeature.Instance.Role_List("", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        RolesModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<RolesModel>(data);
                        if (role.data.Length > 0)
                        {
                            for (int i = 0; i < role.data.Length; i++)
                            {
                                this.RolesList.Add(new RolesModel()
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
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
                    var role = obj as RolesModel;
                    Http.MobilePOS.Personnel.Instance.Insert(PopupChooseRole.user_id, role.Id, "1234", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            PersonnelsViewModel._personnelsList.Clear();
                            PersonnelsViewModel.LoadData();
                            await PopupNavigation.Instance.PopAsync();
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
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }

        public Command<object> DeleteRoleCommand { get; set; }

        private async void DeleteRoleClicked(object obj)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var role = obj as RolesModel;
                        Http.POSFeature.Instance.Role_Delete(role.Id, (bool success, string data) =>
                        {
                            if (success)
                            {
                                LoadListRole();
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }

        public Command<object> UpdateRoleCommand { get; set; }

        private async void UpdateRoleClicked(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new PopupEditRole());
        }
    }
}
