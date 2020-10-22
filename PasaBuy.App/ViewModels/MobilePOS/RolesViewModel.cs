using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class RolesViewModel : BaseViewModel
    {
        public static ObservableCollection<RolesModel> _rolesList;

        public static ObservableCollection<RolesModel> _accessList;

        public ObservableCollection<RolesModel> AccessList
        {
            get
            {
                return _accessList;
            }
            set
            {
                _accessList = value;
                this.NotifyPropertyChanged();
            }
        }

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

        public RolesViewModel()
        {
            this.RolesList = new ObservableCollection<RolesModel>()
            {
                new RolesModel
                {
                    Id = "1",
                    RoleTitle = "Manager"
                },
                new RolesModel
                {
                    Id = "13",
                    RoleTitle = "Cashier"
                }

            };

            this.AccessList = new ObservableCollection<RolesModel>()
            {
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can add product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can edit product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete product"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete category"
                },
                new RolesModel
                {
                    AccessId = "1",
                    AccessName = "Can delete personnel"
                }
            };
        }
    }
}
