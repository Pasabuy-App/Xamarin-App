using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class PersonnelsViewModel : BaseViewModel
    {
        public static ObservableCollection<Personnels> _personnelsList;

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

        public PersonnelsViewModel()
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
        }
    }
}
