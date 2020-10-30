using Forms9Patch;
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
        private ObservableCollection<Personnels> _userList;

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

        public AddPersonnelViewModel()
        {
            _userList = new ObservableCollection<Personnels>();

            for(int i = 0; i < 5; i++)
            {
                _userList.Add(new Personnels()
                {
                    Avatar = "Avatar.png",
                    FullName = "Catriona Gray",
                    User_id = "123",
                });
            }
            
        }

        public ICommand ChooseRoleCommand
        {
            get
            {
                return new Command<string>((x) => ChooseRoleClicked(x));
            }
        }

        private async void ChooseRoleClicked(string id)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                //await App.Current.MainPage.DisplayAlert("Ok", "Selected user id:" + id, "OK");
                await PopupNavigation.Instance.PushAsync(new PopupChooseRole());
                IsBusy = false;
            }

        }
    }
}
