using System.ComponentModel;

namespace PasaBuy.App.Models.MobilePOS
{
    public class Personnels
    {
        public PersonnelsData[] data;
        public class PersonnelsData
        {
            public string ID = string.Empty;
            public string wpid = string.Empty;
            public string avatar = string.Empty;
            public string name = string.Empty;
            public string dname = string.Empty;
            public string display_name = string.Empty;
            public string position = string.Empty;
            public string date_created = string.Empty;
            public string status = string.Empty;
            public string activated = string.Empty;
        }

        private string id = string.Empty;
        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string fullname = string.Empty;
        private string avatar = string.Empty;
        private string position = string.Empty;
        private string date_created = string.Empty;
        private string user_id = string.Empty;

        public string User_id
        {
            get
            {
                return user_id;
            }
            set
            {
                user_id = value;
                OnPropertyChanged("User_id");
            }
        }
        private string activated = string.Empty;

        public string Activated
        {
            get
            {
                return activated;
            }
            set
            {
                activated = value;
                OnPropertyChanged("Activated");
            }
        }
        private string status = string.Empty;

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public string DateCreated
        {
            get
            {
                return date_created;
            }
            set
            {
                date_created = value;
                OnPropertyChanged("DateCreated");
            }
        }

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string FirstName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
                OnPropertyChanged("LastName");
            }
        }

        public string FullName
        {
            get
            {
                return fullname;
            }
            set
            {
                fullname = value;
                OnPropertyChanged("FullName");
            }
        }

        public string Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                avatar = value;
                OnPropertyChanged("Avatar");
            }
        }

        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
