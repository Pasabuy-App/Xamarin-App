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
                Http.POSFeature.Instance.Role_List("", "active", (bool success, string data) =>
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
                                    RoleStatus = role.data[i].status,
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

        public ICommand AddRoleCommand
        {
            get
            {
                return new Command<string>((x) => AddRoleClicked(x));
            }
        }

        private async void AddRoleClicked(string id)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                await App.Current.MainPage.DisplayAlert("Ok", "Selected user role:" + id, "OK");
                await PopupNavigation.Instance.PopAsync();
                await App.Current.MainPage.Navigation.PopModalAsync();
                IsBusy = false;
            }

        }
    }
}
