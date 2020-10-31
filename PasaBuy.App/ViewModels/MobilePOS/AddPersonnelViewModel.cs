using PasaBuy.App.Local;
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
    public class AddPersonnelViewModel : BaseViewModel
    {
        public static ObservableCollection<Personnels> _userList;

        public ObservableCollection<Personnels> UserList
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList = value;
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

        public AddPersonnelViewModel()
        {
            _userList = new ObservableCollection<Personnels>();
            LoadUser();

            RefreshCommand = new Command<string>((key) =>
            {
                _userList.Clear();
                LoadUser();
                IsRefreshing = false;
            });
            ChooseRoleCommand = new Command<object>(ChooseRoleClicked);
        }
        public Command<object> ChooseRoleCommand { get; set; }

        private async void ChooseRoleClicked(object obj)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                var person = obj as Personnels;
                PopupChooseRole.user_id = person.User_id;
                await PopupNavigation.Instance.PushAsync(new PopupChooseRole());
                IsBusy = false;
            }
        }

        public void LoadUser()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.DataFeature.Instance.Search_User("", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Personnels user = Newtonsoft.Json.JsonConvert.DeserializeObject<Personnels>(data);
                            if (user.data.Length > 0)
                            {
                                for (int i = 0; i < user.data.Length; i++)
                                {
                                    _userList.Add(new Personnels()
                                    {
                                        User_id = user.data[i].ID,
                                        Avatar = user.data[i].avatar == "" ? "" : PSAProc.GetUrl(user.data[i].avatar),
                                        FullName = user.data[i].name,
                                    });
                                }
                                IsRunning = false;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", "No user found.", "Try Again");
                                IsRunning = false;
                            }
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsRunning = false;
            }
        }

        public static void SearchUser(string search)
        {
            try
            {
                Http.DataFeature.Instance.Search_User(search, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Personnels user = Newtonsoft.Json.JsonConvert.DeserializeObject<Personnels>(data);
                        if (user.data.Length > 0)
                        {
                            _userList.Clear();
                            for (int i = 0; i < user.data.Length; i++)
                            {
                                _userList.Add(new Personnels()
                                {
                                    User_id = user.data[i].ID,
                                    Avatar = user.data[i].avatar == "" ? "" : PSAProc.GetUrl(user.data[i].avatar),
                                    FullName = user.data[i].name,
                                });
                            }
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", "No user found.", "Try Again");
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
    }
}
