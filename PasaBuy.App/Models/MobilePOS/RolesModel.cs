using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.MobilePOS
{
    public class RolesModel
    {
        public RolesData[] data;
        public class RolesData
        {
            public string ID = string.Empty;
            public string stid = string.Empty;
            public string title = string.Empty;
            public string info = string.Empty;
            public string status = string.Empty;
            public string created_by = string.Empty;
            public string date_created = string.Empty;
        }

        private string role_status = string.Empty;
        private string role_info = string.Empty;
        private string role_title = string.Empty;
        private string id = string.Empty;
        private string access_name = string.Empty;
        private string access_id = string.Empty;

        public ObservableCollection<AccessModel> Access { get; set; }


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

        public string AccessName
        {
            get
            {
                return access_name;
            }
            set
            {
                access_name = value;
                OnPropertyChanged("AccessName");
            }
        }

        public string AccessId
        {
            get
            {
                return access_id;
            }
            set
            {
                access_id = value;
                OnPropertyChanged("AccessId");
            }
        }

        public string RoleTitle
        {
            get
            {
                return role_title;
            }
            set
            {
                role_title = value;
                OnPropertyChanged("RoleTitle");
            }
        }

        public string RoleInfo
        {
            get
            {
                return role_info;
            }
            set
            {
                role_info = value;
                OnPropertyChanged("RoleInfo");
            }
        }

        public string RoleStatus
        {
            get
            {
                return role_status;
            }
            set
            {
                role_status = value;
                OnPropertyChanged("RoleStatus");
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
