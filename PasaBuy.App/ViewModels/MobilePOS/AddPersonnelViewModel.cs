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
        public static ObservableCollection<Models.POSFeature.PersonnelModel> _userList;

        public ObservableCollection<Models.POSFeature.PersonnelModel> UserList
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
            _userList = new ObservableCollection<Models.POSFeature.PersonnelModel>();
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
                var person = obj as Models.POSFeature.PersonnelModel;
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
                    Http.DataVice.Users.Instance.Search_User("", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.POSFeature.PersonnelModel user = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.PersonnelModel>(data);
                            if (user.data.Length > 0)
                            {
                                for (int i = 0; i < user.data.Length; i++)
                                {
                                    _userList.Add(new Models.POSFeature.PersonnelModel()
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1URS-SU1APVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1URS-SU1APVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-SU1APVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1URS-SU1APVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }

        public static void SearchUser(string search)
        {
            try
            {
                Http.DataVice.Users.Instance.Search_User(search, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.POSFeature.PersonnelModel user = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.POSFeature.PersonnelModel>(data);
                        if (user.data.Length > 0)
                        {
                            _userList.Clear();
                            for (int i = 0; i < user.data.Length; i++)
                            {
                                _userList.Add(new Models.POSFeature.PersonnelModel()
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1URS-SU1APVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1URS-SU1APVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-SU1APVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1URS-SU1APVM-" + err.ToString());
                }
            }
        }
    }
}
