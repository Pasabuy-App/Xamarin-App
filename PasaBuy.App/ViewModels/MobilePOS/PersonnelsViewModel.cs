using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    /// <summary>
    /// ViewModel for Personnel page.
    /// </summary>
    public class PersonnelsViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.POSFeature.PersonnelModel> _personnelsList;
        public static ObservableCollection<Models.POSFeature.PersonnelModel> _usersList;
        public static ObservableCollection<Models.POSFeature.RoleModel> _rolesList;
        public ObservableCollection<Models.POSFeature.RoleModel> RolesList
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

        public ObservableCollection<Models.POSFeature.PersonnelModel> PersonnelsList
        {
            get
            {
                return _personnelsList;
            }
            set
            {
                _personnelsList = value;
                this.NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Models.POSFeature.PersonnelModel> UsersList
        {
            get
            {
                return _usersList;
            }
            set
            {
                _usersList = value;
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
        public PersonnelsViewModel()
        {
            _usersList = new ObservableCollection<Models.POSFeature.PersonnelModel>();
            _usersList.Clear();

            this.PersonnelsList = new ObservableCollection<Models.POSFeature.PersonnelModel>();
            LoadPersonnel();

            _personnelsList = new ObservableCollection<Models.POSFeature.PersonnelModel>();
            _rolesList = new ObservableCollection<Models.POSFeature.RoleModel>();

            LoadRoleList();
            ChangeWalletCommand = new Command<object>(ChangeWalletClicked);
            DeleteCommand = new Command<object>(DeleteClicked);
            ViewPersonnelCommand = new Command<object>(ViewPersonnelClicked);
            UpdateCommand = new Command<object>(UpdateClicked);

            RefreshCommand = new Command<string>((key) =>
            {
                _personnelsList.Clear();
                LoadPersonnel();
                IsRefreshing = false;
            });
        }
        public Command<object> ChangeWalletCommand { get; set; }
        private async void ChangeWalletClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Change Wallet?", "Are you sure to change?", "Yes", "No");
                    if (answer)
                    {
                        var person = obj as Models.POSFeature.PersonnelModel;
                        Http.MobilePOS.Wallet.Instance.Change(Views.StoreViews.Management.SearchWalletPersonnel.key, person.User_id, async (bool success, string data) =>
                        {
                            if (success)
                            {
                                PaymentsViewModel._walletTransactions.Clear();
                                await App.Current.MainPage.Navigation.PopModalAsync();
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2WLT-C1PVM.", "OK");
                IsRunning = false;
            }
        }


        public void LoadRoleList()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.MobilePOS.Role.Instance.Listing("", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.RoleModel role = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.RoleModel>(data);

                            for (int i = 0; i < role.data.Length; i++)
                            {
                                _rolesList.Add(new Models.POSFeature.RoleModel()
                                {
                                    Id = role.data[i].ID,
                                    RoleTitle = role.data[i].title,
                                    RoleInfo = role.data[i].info,
                                    RoleStatus = role.data[i].status,
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L1PVM.", "OK");
                IsRunning = false;
            }
        }

        public static void RefreshPersonnel()
        {
            try
            {
                _personnelsList.Clear();
                Http.MobilePOS.Personnel.Instance.Listing("", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.PersonnelModel personnel = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.PersonnelModel>(data);

                        for (int i = 0; i < personnel.data.Length; i++)
                        {
                            string date_created = personnel.data[i].date_created; // "2020-10-11 11:05:07";
                            DateTime datecustom = new DateTime(Convert.ToInt32(date_created.Substring(0, 4)), Convert.ToInt32(date_created.Substring(5, 2)), Convert.ToInt32(date_created.Substring(8, 2)));

                            _personnelsList.Add(new Models.POSFeature.PersonnelModel
                            {
                                Id = personnel.data[i].ID,
                                User_id = personnel.data[i].wpid,
                                Avatar = PSAProc.GetUrl(personnel.data[i].avatar),
                                FullName = personnel.data[i].display_name,
                                Position = personnel.data[i].position,
                                Status = personnel.data[i].status,
                                Activated = personnel.data[i].activated,
                                DateCreated = datecustom.ToString("MMM. dd, yyyy")  // Date format - Oct. 11, 2020
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-L1PVM.", "OK");
            }
        }

        public void LoadPersonnel()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    this.PersonnelsList.Clear();
                    Http.MobilePOS.Personnel.Instance.Listing("", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.PersonnelModel personnel = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.PersonnelModel>(data);

                            for (int i = 0; i < personnel.data.Length; i++)
                            {
                                string date_created = personnel.data[i].date_created; // "2020-10-11 11:05:07";
                                DateTime datecustom = new DateTime(Convert.ToInt32(date_created.Substring(0, 4)), Convert.ToInt32(date_created.Substring(5, 2)), Convert.ToInt32(date_created.Substring(8, 2)));

                                this.PersonnelsList.Add(new Models.POSFeature.PersonnelModel
                                {
                                    Id = personnel.data[i].ID,
                                    User_id = personnel.data[i].wpid,
                                    Avatar = PSAProc.GetUrl(personnel.data[i].avatar),
                                    FullName = personnel.data[i].display_name,
                                    Position = personnel.data[i].position,
                                    Status = personnel.data[i].status,
                                    Activated = personnel.data[i].activated,
                                    DateCreated = datecustom.ToString("MMM. dd, yyyy")  // Date format - Oct. 11, 2020
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;

                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-L2PVM.", "OK");
                IsRunning = false;
            }
        }

        public Command<object> DeleteCommand { get; set; }

        private async void DeleteClicked(object obj)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Personnel?", "Are you sure to delete this?", "Yes", "No");
                    if (answer)
                    {
                        var person = obj as Models.POSFeature.PersonnelModel;
                        Http.MobilePOS.Personnel.Instance.Delete(person.Id, (bool success, string data) =>
                        {
                            if (success)
                            {
                                RefreshPersonnel();
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-D1PVM.", "OK");
                IsRunning = false;
            }
        }
        public Command<object> ViewPersonnelCommand { get; set; }
        private async void ViewPersonnelClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var person = obj as Models.POSFeature.PersonnelModel;
                PopupViewPersonnel.id = person.Id;
                PopupViewPersonnel.name = person.FullName;
                PopupViewPersonnel.position = person.Position;
                PopupViewPersonnel.date = person.DateCreated;
                PopupViewPersonnel.status = person.Status;
                PopupViewPersonnel.activated = person.Activated;
                await PopupNavigation.Instance.PushAsync(new PopupViewPersonnel());
                IsRunning = false;
            }
        }
        public Command<object> UpdateCommand { get; set; }
        private async void UpdateClicked(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var person = obj as Models.POSFeature.PersonnelModel;
                RolesViewModel.personnel_id = person.Id;
                await PopupNavigation.Instance.PushAsync(new PopupChooseRole());
                IsRunning = false;
            }
        }
    }
}
