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
        public static ObservableCollection<Personnels> _personnelsList;
        public static ObservableCollection<Personnels> _usersList;
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

        public ICommand ViewPersonnelCommand
        {
            get
            {
                return new Command<string>((x) => ViewPersonnelClicked(x));
            }
        }
        private async void ViewPersonnelClicked(string id)
        {
            new Alert("Test", id, "Personnel Id");
            await PopupNavigation.Instance.PushAsync(new PopupViewPersonnel());
        }

        public ICommand ChangeWalletCommand
        {
            get
            {
                return new Command<string>((x) => ChangeWalletClicked(x));
            }
        }
        private async void ChangeWalletClicked(string id)
        {
            new Alert("Test", id, "Personnel Id");
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        public ObservableCollection<Personnels> PersonnelsList
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
        public ObservableCollection<Personnels> UsersList
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

        public PersonnelsViewModel()
        {
            _personnelsList = new ObservableCollection<Personnels>();
            _usersList = new ObservableCollection<Personnels>();
            _rolesList = new ObservableCollection<RolesModel>();
            _personnelsList.Clear();
            _usersList.Clear();
            LoadData();
            LoadRoleList();
        }

        public void LoadRoleList()
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

        public static void LoadUser()
        {
            _usersList.Add(new Personnels
            {
                User_id = "1",
                FullName = "Albania"
            });
            _usersList.Add(new Personnels
            {
                User_id = "2",
                FullName = "Algeria"
            });
            _usersList.Add(new Personnels
            {
                User_id = "3",
                FullName = "American Samoa"
            });
            _usersList.Add(new Personnels
            {
                User_id = "4",
                FullName = "Andorra"
            });
            _usersList.Add(new Personnels
            {
                User_id = "5",
                FullName = "Aruba"
            });
            _usersList.Add(new Personnels
            {
                User_id = "6",
                FullName = "Angola"
            });
            _usersList.Add(new Personnels
            {
                User_id = "7",
                FullName = "Argentina"
            });
            _usersList.Add(new Personnels
            {
                User_id = "8",
                FullName = "Armenia"
            });
            _usersList.Add(new Personnels
            {
                User_id = "9",
                FullName = "America"
            });
            _usersList.Add(new Personnels
            {
                User_id = "10",
                FullName = "Lorz Becislao"
            });
            _usersList.Add(new Personnels
            {
                User_id = "11",
                FullName = "Caezar De Castro"
            });
            _usersList.Add(new Personnels
            {
                User_id = "12",
                FullName = "Miguel Radaza"
            });
            _usersList.Add(new Personnels
            {
                User_id = "13",
                FullName = "Russel Ponferrada"
            });
            _usersList.Add(new Personnels
            {
                User_id = "14",
                FullName = "Lando Ernesto"
            });
        }
        public static void LoadData()
        {
            try
            {
                TindaPress.Personnel.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Personnels personnel = Newtonsoft.Json.JsonConvert.DeserializeObject<Personnels>(data);
                        if (personnel.data.Length > 0)
                        {
                            for (int i = 0; i < personnel.data.Length; i++)
                            {
                                string date_created = personnel.data[i].date_created; // "2020-10-11 11:05:07";
                                DateTime datecustom = new DateTime(Convert.ToInt32(date_created.Substring(0, 4)), Convert.ToInt32(date_created.Substring(5, 2)), Convert.ToInt32(date_created.Substring(8, 2)));

                                _personnelsList.Add(new Personnels
                                {
                                    Id = personnel.data[i].ID,
                                    User_id = personnel.data[i].wpid,
                                    Avatar = PSAProc.GetUrl(personnel.data[i].avatar),
                                    FullName = personnel.data[i].dname,
                                    Position = "Manager",
                                    DateCreated = datecustom.ToString("MMM. dd, yyyy")  // Date format - October 11, 2020
                                });
                            }
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /*public void SampleData()
        {
            this.PersonnelsList = new ObservableCollection<Personnels>()
            {
                new Personnels
                {
                    Avatar = "Avatar.png",
                    FullName = "Lorz Becislao",
                    Position = "Janitor"

                },
                new Personnels
                {
                    Avatar = "Avatar.png",
                    FullName = "Miguel Igdalino",
                    Position = "Store Manager"

                },
                new Personnels
                {
                    Avatar = "Avatar.png",
                    FullName = "Russel Twentyseven",
                    Position = "Cashie"

                },
            };
        }*/
    }
}
